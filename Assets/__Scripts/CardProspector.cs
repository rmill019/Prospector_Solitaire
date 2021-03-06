﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is an enum, which defines a type of variable that only has a few possible
// named values. The CardState variable type has one of four values: drawpile, tableau, target and discard

public enum CardState {
	drawpile,
	tableau,
	target,
	discard
}

public class CardProspector : Card {
	// This is how you use the enum CardState
	public CardState			state = CardState.drawpile;
	// The hiddenBy list stores which other cards will keep this one face down
	public List <CardProspector> hiddenBy = new List<CardProspector>();
	// LayoutID matches this card to a layout XML id if it's a tableau card
	public int					layoutID;
	// The SlotDef class stores information pulled in from the LayoutXML <slot>
	public SlotDef				slotDef;

}
