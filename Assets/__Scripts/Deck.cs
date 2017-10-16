using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

	public bool _______________________________;

	public PT_XMLReader					xmlr;
	public List<string>					cardNames;
	public List<Card> 					cards;
	public List<Decorator>				decorators;
	public List<CardDefinition>			cardDefs;
	public Transform					deckAnchor;
	public Dictionary<string,Sprite>	dictSuits;

	// InitDeck is called by Prospector when it is ready
	public void InitDeck (string deckXMLText) {
		ReadDeck (deckXMLText);
	}

	// ReadDeck parses the XML file passed to it into CardDefinitions
	public void ReadDeck (string deckXMLText) {
		xmlr = new PT_XMLReader ();			// Create a new PT_XMLReader
		xmlr.Parse(deckXMLText);			// Use that PT_XMLReader to parse DeckXML

		// This prints a test line to show you how xmlr can be used
		// For more information read about XML in the Useful concepts Appendis
		string s = "xml[0] decorator[0] ";
		s += "type=" + xmlr.xml ["xml"] [0] ["decorator"] [0].att ("type");
		s += " x=" + xmlr.xml ["xml"] [0] ["decorator"] [0].att ("x");
		s += "y=" + xmlr.xml ["xml"] [0] ["decorator"] [0].att ("y");
		s += " scale=" + xmlr.xml ["xml"] [0] ["decorator"] [0].att ("scale");
		//print (s); This is a test line to make sure that the s String is being filled correctly

		// Read decorators for all Cards
		decorators = new List<Decorator>();		// Initialize the List of Decorators
		// Grab a PT_XMLHashList of all <decorator>s in the XML file
		PT_XMLHashList xDecos = xmlr.xml["xml"] [0] ["decorator"];
		Decorator deco;
		for (int i = 0; i < xDecos.Count; i++) 
		{
			// For each <decorator> in the XML
			deco = new Decorator();		// Make a new Decorator
			// Copy the attributes of the <decorator> to the Decorator
			deco.type = xDecos[i].att("type");
			// Set the bool flip based on whether the text of the attribute is
			// "1" or something else. This is an atypical but perfectly fine
			// use of the == comparison operator. It will return a true or
			// false, which will be assigned to deco.flip.
			deco.flip = (xDecos[i].att ("flip") == "1");
			// floats need to be parsed from the attribute strings
			deco.scale = float.Parse (xDecos[i].att ("scale"));
			// Vector3 loc initializes to [0, 0, 0], so we just need to modify it
			deco.loc.x = float.Parse (xDecos[i].att ("x"));
			deco.loc.y = float.Parse (xDecos [i].att ("y"));
			deco.loc.z = float.Parse (xDecos [i].att ("z"));
			// Add the temporary deco to the List Decorators
			decorators.Add (deco);
		}

		// Read pip locations for each card number
		cardDefs = new List<CardDefinition>();		// Init the List of Cards
		// Grab a PT_XMLHashList of all the <card>s in the XML file
		PT_XMLHashList xCardDefs = xmlr.xml ["xml"] [0] ["card"];
		for (int i = 0; i < xCardDefs.Count; i++) 
		{
			// For each of the <card>s
			// Create a new CardDefinition
			CardDefinition cDef = new CardDefinition();
			// Parse the attribute values and add them to cDef;
			cDef.rank = int.Parse (xCardDefs[i].att ("rank"));
			// Grab a PT_XMLHashList of all the <pip>s on this <card>
			PT_XMLHashList xPips = xCardDefs[i] ["pips"];
		}
	}
}
