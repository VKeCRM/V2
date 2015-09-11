using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	public enum EventType
	{
		//Fires when a control loses the input focus and its value has been modified since gaining focus.
		Change,
		//Fires when a mouse click is detected with the element.
		Click,
		//Fires when a mouse double click is detected with the element.
		DBClick,
		//Fires when an element receives focus either via the pointing device or by tab navigation.
		Focus,
		//Fires when a keydown is detected with the element.
		KeyDown,
		//Fires when a keypress is detected with the element.
		KeyPress,
		//Fires when a keyup is detected with the element.
		KeyUp,
		//Fires when a user selects some text in a text field, including input and textarea.
		Select,
		//Fires when a form is submitted.
		Submit
	}
}
