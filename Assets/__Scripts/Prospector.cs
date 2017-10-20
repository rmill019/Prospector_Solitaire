using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prospector : MonoBehaviour {

	static public Prospector 	S;

	public Deck				 	deck;
	public TextAsset 			deckXML;
	public Vector3				layoutCenter;
	public float				xOffset = 3f;
	public float 				yOffset = -2.5f;
	public Transform			layoutAnchor;

	public CardProspector		target;
	public List<CardProspector>	tableau;
	public List<CardProspector> discardPile;

	public Layout 				layout;
	public TextAsset			layoutXML;


	void Awake () {
		S = this;		// Set up a Singleton for Prospector
	}

	public List<CardProspector>		drawPile;

	void Start () {
		deck = GetComponent<Deck> ();		// Get the Deck
		deck.InitDeck (deckXML.text);		// Pass DeckXML to it

		Deck.Shuffle (ref deck.cards);		// This shuffles the deck;
		// The ref keyword passes a reference to deck.cards, which allows
		// deck.cards to be modified by Deck.Shuffle();

		layout = GetComponent<Layout> ();
		layout.ReadLayout (layoutXML.text);		// pass LayoutXML to layout;

		drawPile = ConvertListCardsToListCardProspectors (deck.cards);
		LayoutGame ();
	}

	// The Draw Function will pull a single card from the drawPile and return it
	CardProspector Draw()
	{
		CardProspector cd = drawPile [0];		// Pull the 0th CardProspector
		drawPile.RemoveAt(0);					// Then remove it from List<> drawPile
		return (cd);
	}


	// LayoutGame () positions the initial tableau of cards, a.k.a the "mine"
	void LayoutGame ()
	{
		// Create an empty GameObject to serve as an anchor for the tableau
		if (layoutAnchor == null)
		{
			// Create an empty GameObject named _LayoutAnchor in the Hierarchy
			GameObject tGO = new GameObject ("_LayoutAnchor");
			layoutAnchor = tGO.transform;		// Grab its Transform
			layoutAnchor.transform.position = layoutCenter;

		}

		CardProspector cp;
		// Follow the layout
		// Iterate through all the SlotDefs in the layout.SlotDefs as tSD
		foreach (SlotDef tSD in layout.slotDefs)
		{
			cp = Draw ();	// Pull a card from the top (beginning) of the drawPile
			cp.faceUp = tSD.faceUp;
			cp.transform.parent = layoutAnchor;		// Make its parent layoutAnchor
			// This replaces the previous parent: deck.deckAnchor, which appears as _Deck
			// in the hierarchy when the scene is playing
			cp.transform.localPosition = new Vector3 (layout.multiplier.x * tSD.x,
													  layout.multiplier.y * tSD.y,
													  -tSD.layerID);
			cp.layoutID = tSD.id;
			cp.slotDef = tSD;
			cp.state = CardState.tableau;
			// CardProspectors in the tableau have the state CardState.tableau

			tableau.Add (cp);	// Add this CardProspector to the List<> tableau
		}
	}



	List<CardProspector> ConvertListCardsToListCardProspectors (List<Card> lCD)
	{
		List<CardProspector> lCP = new List<CardProspector> ();
		CardProspector tCP;
		foreach (Card tCD in lCD)
		{
			tCP = tCD as CardProspector;
			lCP.Add (tCP);
		}
		return (lCP);
	}
}
