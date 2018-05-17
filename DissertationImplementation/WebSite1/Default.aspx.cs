using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;


public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void recommendationButton_Click(object sender, EventArgs e)
    {
        //when button pressed reset labels so that the system can be used multiple times without needing to refresh the page
        Label1.Text = " ";
        Label2.Text = " ";
        Label3.Text = " ";
        Label4.Text = " ";
        Label5.Text = " ";
        Label6.Text = " ";
        Label7.Text = " ";
        Label8.Text = " ";
        Label9.Text = " ";
        Label10.Text = " ";
        Label12.Text = "";
        //6 arrays, first through fifth are for keeping track of the individual artist's tags, overlapping keeps track of all of the tags that appear on all of the artists
        String[] overlappingTags = new string[10];
        String[] firstTags = new string[10];
        String[] secondTags = new string[10];
        String[] thirdTags = new string[10];
        String[] fourthTags = new string[10];
        String[] fifthTags = new string[10];
        //finaltags used for actually 
        List<String> finalTags  = new List<string>();
        // most of the program takes place in try catch blocks because it isn't uncommon for this program to try to refer to something that doesn't exist and this lets it do that without crashing
        try
        {
            Label1.Text = artist1TXT.Text +"'s tags = ";
            //xdocument retrieves a copy of the XML file containing this artist's top tags and stores it locally
            XDocument artist1Tags = XDocument.Load("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptags&artist=" + artist1TXT.Text + "&api_key=1d867187a0242df9e015069588cd1ff2");
            for (int i = 0; i < 10; i++)
            {
                //to gather information on an element you have to use the .element() method. elements can have elements beneath them which means that you might need a chain of .element() method calls as seen below
                String artist1tag = artist1Tags.Element("lfm").Element("toptags").Element("tag").Element("name").Value;
                //overlappingTags is setup as a version of firsttags that won't get changed
                overlappingTags[i] = artist1tag;
                firstTags[i] = artist1tag;
                //this ensures that if the user only enters one artist they can get recommendations based on that one artist
                if (artist2TXT.Text == "" && artist3TXT.Text == "" && artist4TXT.Text == "" && artist5TXT.Text == "")
                {
                    finalTags.Add(artist1tag);
                }
                //this prints the artist's tags for debugging purposes
                    Label1.Text = Label1.Text + firstTags[i];
                if (i != 10) {
                    Label1.Text = Label1.Text + ", ";
                }
                // this means that every time we perform this loop we don't use the same tag because each tag is deleted after we have used it
                artist1Tags.Element("lfm").Element("toptags").Element("tag").Remove();
            }
            
            if(artist2TXT.Text == "" && artist3TXT.Text == "" && artist4TXT.Text == "" && artist5TXT.Text == "")
            {
               
                for (int i = 0; i<5; i++)
                { //this prints the recommendations based on a single artist that we established earlier. PrintRecs is a custom method found at the bottom of this page.
                    PrintRecs(finalTags);
                }
            }
        }
        catch { }
        if (artist2TXT.Text != "")
        {
            try
            {
                Label2.Text = artist2TXT.Text + "'s tags = ";
                XDocument artist2Tags = XDocument.Load("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptags&artist=" + artist2TXT.Text + "&api_key=1d867187a0242df9e015069588cd1ff2");
                // this array keeps track of any tags that overlap with previous artists
                Boolean[] overlap = new Boolean[10];
                for (int i = 0; i < 10; i++)
                {

                    String artist2tag = artist2Tags.Element("lfm").Element("toptags").Element("tag").Element("name").Value;

                    for (int k = 0; k < 10; k++)
                    {
                        //this lets us use the overlap array to keep track of which tags overlap. the overlap array and the artist2tag array run directly parallel so overlap[4] will always apply to artist2tag[4]
                        if (overlappingTags[k] == artist2tag)
                        {
                            overlap[i] = true;
                        }
                    }
                    secondTags[i] = artist2tag;
                    Label2.Text = Label2.Text + artist2tag;
                    if (i != 10)
                    {
                        Label2.Text = Label2.Text + ", ";
                    }
                    artist2Tags.Element("lfm").Element("toptags").Element("tag").Remove();
                }
                for (int i = 0; i < 10; i++)
                {
                    if (!overlap[i])
                    {// this removes any overlapping tags from the secondtags array which lets us accurately fill the final tags list
                        secondTags[i] = null;
                    }
                }
                if (artist3TXT.Text == "" && artist4TXT.Text == "" && artist5TXT.Text == "") //this guarantees that if the later textboxes aren't filled in then the program will still print results
                {
                    // at various parts of the program we filter out the tag seen live. this is because it's a tag that makes sense in the context of Last.FM but only ends up producing false positives for our purposes.
                    // If we don't filter it then the program will recommend The National almost every time  
                    for (int i = 0; i < 10; i++)
                    {
                        if (secondTags[i] != null && secondTags[i] != "seen live")
                        {
                            finalTags.Add(secondTags[i]);
                        }
                    }
                    PrintRecs(finalTags);
                }
                
            }
            catch { }
        }
        if (artist3TXT.Text != "")
        {
            try
            { //most of the code in the next few blocks are just repeating what we did in the previous block for a new artist so I won't take the time to comment each block with the same things
                Label3.Text = artist3TXT.Text + "'s tags = ";
                XDocument artist3Tags = XDocument.Load("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptags&artist=" + artist3TXT.Text + "&api_key=1d867187a0242df9e015069588cd1ff2");
                Boolean[] overlap = new Boolean[10];
                Boolean[] overlap2 = new Boolean[10];
                for (int i = 0; i < 10; i++)
                {

                    String artist3tag = artist3Tags.Element("lfm").Element("toptags").Element("tag").Element("name").Value;

                    for (int k = 0; k < 10; k++)
                    {//when checking for overlaps we compare each tag with each of the previous arrays
                        if (overlappingTags[k] == artist3tag)
                        {
                            overlap[i] = true;
                        }
                        if (secondTags[k] == artist3tag)
                        {
                            overlap2[i] = true;
                        }
                    }
                    thirdTags[i] = artist3tag;
                    Label3.Text = Label3.Text + artist3tag;
                    if (i != 10)
                    {
                        Label3.Text = Label3.Text + ", ";
                    }
                    artist3Tags.Element("lfm").Element("toptags").Element("tag").Remove();
                }
                for (int i = 0; i < 10; i++)
                {
                    if (!overlap[i] || !overlap2[i])
                    {
                        thirdTags[i] = null;
                    }
                }

                if (artist4TXT.Text == "" && artist5TXT.Text == "")
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (thirdTags[i] != null && thirdTags[i] != "seen live")
                        {
                            finalTags.Add(thirdTags[i]);
                        }
                    }
                    PrintRecs(finalTags);
                }
                
            }
            catch { }
        }
        if (artist4TXT.Text != "")
        {
            try
            {
                Label4.Text = artist4TXT.Text + "'s tags = ";
                XDocument artist4Tags = XDocument.Load("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptags&artist=" + artist4TXT.Text + "&api_key=1d867187a0242df9e015069588cd1ff2");
                Boolean[] overlap = new Boolean[10];
                Boolean[] overlap2 = new Boolean[10];
                Boolean[] overlap3 = new Boolean[10];
                for (int i = 0; i < 10; i++)
                {

                    String artist4tag = artist4Tags.Element("lfm").Element("toptags").Element("tag").Element("name").Value;

                    for (int k = 0; k < 10; k++)
                    {
                        if (overlappingTags[k] == artist4tag)
                        {
                            overlap[i] = true;
                        }
                        if (secondTags[k] == artist4tag)
                        {
                            overlap2[i] = true;
                        }
                        if (thirdTags[k] == artist4tag)
                        {
                            overlap3[i] = true;
                        }
                    }
                    fourthTags[i] = artist4tag;
                    Label4.Text = Label4.Text + artist4tag;
                    if (i != 10)
                    {
                        Label4.Text = Label4.Text + ", ";
                    }
                    artist4Tags.Element("lfm").Element("toptags").Element("tag").Remove();
                }
                for (int i = 0; i < 10; i++)
                {
                    if (!overlap[i] || !overlap2[i] || !overlap3[i])
                    {
                        fourthTags[i] = null;
                    }
                }
                if (artist5TXT.Text == "")
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (fourthTags[i] != null && fourthTags[i] != "seen live")
                        {
                            finalTags.Add(fourthTags[i]);
                        }
                    }
                    PrintRecs(finalTags);
                }
                
            }
            catch { }
        }
        if (artist5TXT.Text != "")
        {
            try
            {
                Label5.Text = artist5TXT.Text + "'s tags = ";
                XDocument artist5Tags = XDocument.Load("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptags&artist=" + artist5TXT.Text + "&api_key=1d867187a0242df9e015069588cd1ff2");
                Boolean[] overlap = new Boolean[10];
                Boolean[] overlap2 = new Boolean[10];
                Boolean[] overlap3 = new Boolean[10];
                Boolean[] overlap4 = new Boolean[10];
                for (int i = 0; i < 10; i++)
                {

                    String artist5tag = artist5Tags.Element("lfm").Element("toptags").Element("tag").Element("name").Value;

                    for (int k = 0; k < 10; k++)
                    {
                        if (overlappingTags[k] == artist5tag)
                        {
                            overlap[i] = true;
                        }
                        if (secondTags[k] == artist5tag)
                        {
                            overlap2[i] = true;
                        }
                        if (thirdTags[k] == artist5tag)
                        {
                            overlap3[i] = true;
                        }
                        if (fourthTags[k] == artist5tag)
                        {
                            overlap4[i] = true;
                        }
                    }
                    fifthTags[i] = artist5tag;
                    Label5.Text = Label5.Text + artist5tag;
                    if (i != 10)
                    {
                        Label5.Text = Label5.Text + ", ";
                    }
                    artist5Tags.Element("lfm").Element("toptags").Element("tag").Remove();
                }
                for (int i = 0; i < 10; i++)
                {
                    if (!overlap[i] || !overlap2[i] || !overlap3[i] || !overlap4[i])
                    {
                        fifthTags[i] = null;
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    if (fifthTags[i] != null)
                    {
                        finalTags.Add(fifthTags[i]);
                    }
                }
                PrintRecs(finalTags);
            }
            catch { }

        }
        try
        {
            for (int i = 0; i < 10; i++)
            {
                if (i != 0 && finalTags[i] != null && finalTags[i] != "seen live") { Label12.Text = Label12.Text + ", "; }
                if (finalTags[i] != null && finalTags[i] != "seen live") { Label12.Text = Label12.Text + finalTags[i]; }
            }
        }
        catch { }
        if (Label12.Text == ""&& artist2TXT.Text != "" ) // this is used if the user enters artists that have no overlap or if the only overlap is "seen live" which will have been filtered out
        {
            Label12.Text = "we didn't find any overlapping qualities between the artists you entered, please try again with a different set of artists";
        }

    }

    public void PrintRecs(List<String> tags)
    {
        // to allow the user greater customisation while using the program we let them choose how well known they want the results to be. this is quantified by how deep in the results they appear.
        // the obscurity Int is assigned a value based on the user's choice and then when we call the tag.gettopartists method from the API the page that we call is the value assigned to obscurity
        int obscurity = new int();
        if (DropDownList1.Text == "well known")
        {
            obscurity = 1;
        } else if (DropDownList1.Text == "somewhat obscure"){
            obscurity = 3;
        } else
        {
            obscurity = 5;
        }

            for (int i=0; i<5; i++)
        {
            if (tags[i] != "seen live")
            {
                XDocument artists = XDocument.Load("http://ws.audioscrobbler.com/2.0/?method=tag.gettopartists&tag=" + tags[i] + "&page=" + obscurity + "&api_key=1d867187a0242df9e015069588cd1ff2");

                if (i == 0)
                {// when displaying results we show the name of the artist and link to the last.fm page
                    Label6.Text = artists.Element("lfm").Element("topartists").Element("artist").Element("name").Value + " - " + artists.Element("lfm").Element("topartists").Element("artist").Element("url").Value;
                }
                if (i == 1)
                {
                    Label7.Text = artists.Element("lfm").Element("topartists").Element("artist").Element("name").Value + " - " + artists.Element("lfm").Element("topartists").Element("artist").Element("url").Value;
                }
                if (i == 2)
                {
                    Label8.Text = artists.Element("lfm").Element("topartists").Element("artist").Element("name").Value + " - " + artists.Element("lfm").Element("topartists").Element("artist").Element("url").Value;
                }
                if (i == 3)
                {
                    Label9.Text = artists.Element("lfm").Element("topartists").Element("artist").Element("name").Value + " - " + artists.Element("lfm").Element("topartists").Element("artist").Element("url").Value;
                }
                if (i == 4)
                {
                    Label10.Text = artists.Element("lfm").Element("topartists").Element("artist").Element("name").Value + " - " + artists.Element("lfm").Element("topartists").Element("artist").Element("url").Value;
                }
            }
        }
    }



    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}