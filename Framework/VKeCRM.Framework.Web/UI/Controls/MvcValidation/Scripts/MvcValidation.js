

function VKeCRMMvcValidation(valId)
{
	this.validator = document.getElementById(valId);

	if (this.validator == null)
	{
		throw "cannot find control with id [" + valId + "]!";
	}

	this.isDebug = this.validator.getAttribute("debug") == "true" || this.validator.getAttribute("debug") == "1" || false;

	this.eventsMapping = {};

	this.Event_OnLoad = "onLoad";
	this.Event_OnError = "onError";
	this.Event_OnSuccess = "onSuccess";
	this.Event_OnValidate = "onValidate";
	this.Event_OnClearError = "onClearError";
	this.Event_OnBeforeValidate = "onBeforeValidate";
}

VKeCRMMvcValidation.prototype =
{
	validation_load: function() {
		this.fire(this.Event_OnLoad, this.validator, null);

		this.init_ctlExtender();
	},

	init_ctlExtender: function() {
		var validators = this.validator.childNodes;
		var doInternal = (this.validator.getAttribute("AllowExpressionSwitch") != "external");

		for (var i = 0; i < validators.length; i++) {
			var val = validators[i];

			var isPreventPaste = val.getAttribute("IsPreventPaste") == "1" || val.getAttribute("IsPreventPaste") == "true" || false;
			if (isPreventPaste == true) {
				var target = this.get_target(val.getAttribute("ControlToValidate"), val.getAttribute("property"), null);
				target.setAttribute("onpaste", "return false;");
			}

			var allowExpression = val.getAttribute("AllowExpression");
			if (allowExpression != null && allowExpression != "") {
				var target = this.get_target(val.getAttribute("ControlToValidate"), val.getAttribute("property"), null);
				target.setAttribute("allowExpression", allowExpression);

				if (doInternal == true) {
					$(target).keypress(function(e) {
						var inputChar = String.fromCharCode(e.which);

						if (e.which == 13 || e.which == 0 || e.which == 8 || !inputChar || inputChar == "") {
							return true;
						}
						else {
							var expression = $(this).attr("allowExpression");

							var pattern = new RegExp(expression.htmlDecode());
							return pattern.test(inputChar);
						}
					});
				}
			}

			var waterMark = val.getAttribute("WaterMark");
			if (waterMark != null && waterMark != "") {
				var target = this.get_target(val.getAttribute("ControlToValidate"), val.getAttribute("property"), null);
				target.setAttribute("WaterMark", waterMark);

				$(target).focus(function(e) {
					if ($(this).attr("WaterMark") == $(this).val())
						$(this).val('');

					$(this).removeClass('WaterMark');
				});

				$(target).blur(function(e) {
					if ($(this).val() == '' || $(this).val() == $(this).attr("WaterMark")) {
						$(this).val($(this).attr("WaterMark"));
						$(this).addClass('WaterMark');
					}
				});
			}
		}
	},

	get_target: function(targetId, property, listIndex) {
		if (targetId == null || "" == targetId) {
			var idProvider = this.validator.getAttribute("IdProvider");

			if (null == idProvider)
				throw "Please provider an IdProvider for mvc validation!";

			var args = property + ((listIndex == 0 || null == listIndex) ? "" : listIndex);

			var fnProvider = idProvider + "('" + args + "')";

			targetId = eval(fnProvider);
		}

		var target = document.getElementById(targetId);

		if (target == null) {
			throw "cannot find target with id [" + (targetId || property) + "].";
		}

		return target;
	},

	validate: function(json) {
		// exec default beforeValidate method
		var beforeValidate = this.validator.getAttribute("BeforeValidate");
		var beforeValidateEventArgs = new Object();
		if (null != beforeValidate && "" != beforeValidate) {
			eval(beforeValidate + "(beforeValidateEventArgs)");
		}

		// exec registered method(s)
		this.fire(this.Event_OnBeforeValidate, this.validator, beforeValidateEventArgs);

		json = json || beforeValidateEventArgs || new Object();

		// get all child nodes
		var validators = this.validator.childNodes;

		// clear all previous error messages
		this.clear_errors();

		if (this.validator.getAttribute("CheckAtClient") == "1" ||
			this.validator.getAttribute("CheckAtClient") == "true") {
			var hash = new Object();
			var errorList = [];

			for (var i = 0; i < validators.length; i++) {
				var tempVal = validators[i];

				// check if it is ingored
				var ingoreTaret = tempVal.getAttribute("IngoreTaret");
				var ingoreValue = tempVal.getAttribute("IngoreValue");

				var parent = tempVal.getAttribute("Parent");
				if (null != parent) {
					var dataArr = json[parent];

					if (null == dataArr) {
						// todo
					}
					else {
						var isArray = (null != dataArr.length);
						if (isArray) {
							for (var k = 0; k < dataArr.length; k++) {
								var iJson = dataArr[k];

								// check ingore
								if (null != ingoreTaret && null != ingoreValue && null != iJson[ingoreTaret] &&
								   (iJson[ingoreTaret] + "").toLowerCase() == ingoreValue.toLowerCase()) {
									continue;
								}

								var property = tempVal.getAttribute("property");
								// if previous validate has error
								// no need to validate for this property again.
								var keyFlag = parent + "|" + property + "|" + k;
								if (!hash[keyFlag]) {
									var valResult = this.do_validate(tempVal, iJson, k);
									if (!valResult.IsValid) {
										// flag
										hash[keyFlag] = 1;

										var toError = { PropertyName: valResult.Property, ErrorMessage: valResult.ErrorMessage, MvcValidationType: valResult.ValType, Parent: parent, ListIndex: k };
										errorList.push(toError);
									}
								}
							}
						}
						else {
							throw "Only support array now, not support object.";
						}
					}
				}
				else {
					// check ingore
					if (null != ingoreTaret && null != ingoreValue && null != json[ingoreTaret] &&
					   (json[ingoreTaret] + "").toLowerCase() == ingoreValue.toLowerCase()) {
						continue;
					}

					var property = tempVal.getAttribute("property");
					// if previous validate has error
					// no need to validate for this property again.
					if (!hash[property]) {
						var valResult = this.do_validate(tempVal, json);
						if (!valResult.IsValid) {
							// flag
							hash[valResult.Property] = 1;

							var toError = { PropertyName: valResult.Property, ErrorMessage: valResult.ErrorMessage, MvcValidationType: valResult.ValType };
							errorList.push(toError);
						}
					}
				}
			}

			var customerValidate = this.validator.getAttribute("CustomerValidate");
			if (null != customerValidate && "" != customerValidate)
				eval(customerValidate + "(errorList)");

			// has errors
			if (errorList.length > 0) {
				return this.call_onError(errorList);
			}
			else {
				return this.call_onSuccess(json);
			}
		}
		else {
			return this.call_onSuccess(json);
		}
	},

	call_onSuccess: function(json) {
		var defautOnSuccess = this.validator.getAttribute("OnSuccess");
		if (null != defautOnSuccess && "" != defautOnSuccess) {
			try {
				eval(defautOnSuccess + "(this.validator, json)");
			}
			catch (e) {
				if (this.isDebug) {
					alert(e);
				}
			}
		}

		this.fire(this.Event_OnSuccess, this.validator, json);

		return true;
	},

	call_onError: function(errorList) {
		this.show_errors(errorList);

		var args = { Error: VKeCRMSys.getInstance().ErrorType.ValidationFailedAtClient, DataSource: errorList };

		var defautOnError = this.validator.getAttribute("OnError");
		if (null != defautOnError && "" != defautOnError) {
			try {
				eval(defautOnError + "(VKeCRMSys.getInstance().ErrorType.ValidationFailedAtClient, args)");
			}
			catch (e) {
				if (this.isDebug) {
					alert(e);
				}
			}
		}

		this.fire(this.Event_OnError, VKeCRMSys.getInstance().ErrorType.ValidationFailedAtClient, args);

		return false;
	},

	// this errorList contains all hidden fields
	show_errors: function(errorList) {
		var validators = this.validator.childNodes;

		for (var i = 0; i < errorList.length; i++) {
			var property = errorList[i].PropertyName;
			var errorMessage = errorList[i].ErrorMessage;
			var valType = errorList[i].MvcValidationType;

			var parent = errorList[i].Parent;
			var listIndex = errorList[i].ListIndex;
			var showInline = errorList[i].ShowInline;

			// to support old application, showInline may be null, so need to check false
			if (showInline == false)
				continue;

			// first use IdProvider, then use mapping
			try {
				var target = this.get_target(null, property, listIndex);

				if (null == target)
					throw "";

				this.show_inline(target, errorMessage);
			}
			catch (e) {
				var temp = null;
				for (var j = 0; j < validators.length; j++) {
					var val = validators[j];

					if (property == val.getAttribute("property") &&
							(parent == null || parent == val.getAttribute("Parent")) &&
							(valType == null || valType == "" || valType == val.getAttribute("ValidateType"))) {
						temp = val;
						break;
					}
				}

				if (null == temp)
					throw "Cannnot find mapping for property: " + property + ", ValidateType: " + valType;

				var target = this.get_target(temp.getAttribute("ControlToValidate"), temp.getAttribute("property"), null);
				this.show_inline(target, errorMessage);
			}
		}
	},

	show_inline: function(targetCtrl, errorMessage) {
		if (targetCtrl && targetCtrl.parentNode) {
			// check if already append error messages
			var spans = targetCtrl.parentNode.getElementsByTagName("div");
			if (spans.length > 0) {
				for (var i = 0; i < spans.length; i++) {
					if (spans[i].getAttribute("type") == "inline") {
						return;
					}
				}
			}

			var images = targetCtrl.parentNode.getElementsByTagName("img");
			if (images.length > 0) {
				for (var i = 0; i < images.length; i++) {
					if (images[i].getAttribute("type") == "inline") {
						return;
					}
				}
			}

			// set target control style
			targetCtrl.style.borderColor = "#e94592";

			// set error message show
			// 1. add error img
			var errorImg = document.createElement("img");
			errorImg.setAttribute("type", "inline");
			errorImg.src = 'https://c528672.ssl.cf2.rackcdn.com/assets/graphics/icon_exclamation_sm.gif';
			errorImg.style.border = "none";
			errorImg.style.position = "relative";
			errorImg.style.top = "3px";
			//errorImg.style.paddingRight = "5px";
			//errorImg.style.styleFloat = "left";

			//errorImg.setAttribute("style", "float:left; padding-right:5px;");
			targetCtrl.parentNode.appendChild(errorImg);

			// 2. add error message.
			var errorSpan = document.createElement('div');
			errorSpan.setAttribute("type", "inline");
			errorSpan.className = "inline-error";
			errorSpan.innerHTML = errorMessage;

			targetCtrl.parentNode.appendChild(errorSpan);
		}
	},

	clear_errors: function(validators) {
		var validators = this.validator.childNodes;
		var originalBorderColor = this.validator.getAttribute('OriginalBorderColor');

		for (var k = 0; k < validators.length; k++) {
			var val = validators[k];

			var eventArgs = new Object();

			eventArgs.Cancel = false;
			// fire event
			this.fire(this.Event_OnClearError, val, eventArgs);

			if (!eventArgs.Cancel) {
				try {
					var target = this.get_target(val.getAttribute("ControlToValidate"), val.getAttribute("property"), null);
					this.clear_inline(target, originalBorderColor);

					// clear others
					if (val.getAttribute("Parent") != null) {
						var baseId = target.getAttribute("id");
						if (null != baseId) {
							for (var x = 1; x < 10; x++) {
								var oTarget = document.getElementById(baseId + x);
								if (null == oTarget)
									break;
								else
									this.clear_inline(oTarget, originalBorderColor);
							}
						}
					}
				}
				catch (e) {
					// this should not throw exception
					// some properties are used as flags.
				}
			}
		}
	},

	clear_inline: function(target, originalBorderColor) {
		if (target) {
			// reset border color
			target.style.borderColor = (originalBorderColor != null && originalBorderColor != '') ? originalBorderColor : "#999999";
			// remove error messages show
			if (target.parentNode) {
				var list = new Array();

				var images = target.parentNode.getElementsByTagName("img");
				for (var i = 0; i < images.length; i++) {
					if (images[i].getAttribute("type") == "inline") {
						list.push(images[i]);
					}
				}

				var spans = target.parentNode.getElementsByTagName("div");
				for (var i = 0; i < spans.length; i++) {
					if (spans[i].getAttribute("type") == "inline") {
						list.push(spans[i]);
					}
				}

				var len = list.length;
				for (var i = 0; i < len; i++) {
					var obj = list.pop();
					if (obj) {
						target.parentNode.removeChild(obj);
					}
				}
			}
		}
	},

	do_validate: function(val, json, listIndex) {
		var e = new Object();

		e.IsValid = true;
		e.Property = val.getAttribute("property");
		e.ValType = val.getAttribute("ValidateType");
		e.ErrorMessage = val.getAttribute("ErrorMessage");
		e.Value = (null == json[e.Property]) ? "" : (json[e.Property] + ""); // conver to string
		e.Parent = val.getAttribute("Parent");
		e.ListIndex = listIndex;
		e.Json = json;

		// fire event
		this.fire(this.Event_OnValidate, val, e);

		switch (e.ValType) {
			case "MvcRequired":
				this.validate_required(val, e);
				break;

			case "MvcRegularExpression":
				this.validate_regularExpression(val, e);
				break;

			case "MvcDateFormat":
				this.validate_dateFormat(val, e);
				break;

			case "MvcCompare":
				this.validate_compare(val, e, json);
				break;

			case "MvcTextFilter":
				this.validate_textFilter(val, e);
				break;

			case "MvcCustomer":
				this.validate_customer(val, e);
				break;
		}

		return e;
	},

	validate_required: function(val, e) {
		var initValue = val.getAttribute("InitialValue");

		e.IsValid = (e.Value != null && e.Value != "");

		if (e.IsValid && null != initValue && "" != initValue) {
			e.IsValid = (e.Value != initValue);
		}
	},

	validate_regularExpression: function(val, e) {
		e.Pattern = val.getAttribute("Pattern");

		e.IsValid = (e.Pattern != null && e.Pattern != "");

		if (e.IsValid) {
			var reg = new RegExp(e.Pattern, "i");
			e.IsValid = reg.test(e.Value);
		}
	},

	validate_dateFormat: function(val, e) {
		e.DateFormatString = val.getAttribute("DateFormat");
		e.IsValid = (e.Value == null || e.Value == "");

		if (!e.IsValid) {
			// pre check and fill "0" if needs
			var temp = e.Value.replace(/[0-9]+/gi, "");

			// use the same format char
			if (temp.length == 2 && temp.charAt(0) == temp.charAt(1)) {
				var numberaArray = e.Value.split(temp.charAt(0));
				var newDateText = "";

				for (var i = 0; i < numberaArray.length; i++) {
					if (numberaArray[i].length == 1) {
						numberaArray[i] = "0" + numberaArray[i];
					}

					newDateText += numberaArray[i];

					if (i != numberaArray.length - 1) {
						newDateText += temp.charAt(0);
					}
				}

				e.Value = newDateText;
			}

			var dateFormats = e.DateFormatString.split(',');

			for (var i = 0; i < dateFormats.length; i++) {
				if (dateFormats[i] != "") {
					if (isValidDate(e.Value, dateFormats[i], e)) {
						e.IsValid = true;
						//replace to new string text when the format is ok.
						//theCtrl.val(dateText);
						// todo
						break;
					}
				}
			}

			// set error message via erro code
			if (!e.IsValid) {
				// 5. year should between 1900 - 9999.
				if (e.ErrorType == 5) {
					e.ErrorMessage = val.getAttribute("MessageOfInValidYear");
				}
				// 6. month is not valid.
				// 7. day is not valid.
				else if (e.ErrorType == 6 || e.ErrorType == 7) {
					e.ErrorMessage = val.getAttribute("MessageOfInValidDate");
				}
				else {
					e.ErrorMessage = val.getAttribute("CommonErrorMessage");
				}
			}
		}
	},

	validate_customer: function(val, e) {
		e.ValidateEmptyText = (val.getAttribute("ValidateEmptyText") == "1");
		e.ClientValidationFunction = val.getAttribute("ClientValidationFunction");

		if (!e.ValidateEmptyText && (null == e.Value || "" == e.Value)) {
			e.IsValid = true;
		}
		else {
			try {
				var result = eval(e.ClientValidationFunction + "(val, e)");
				e.IsValid = (null == result) ? true : result;
			}
			catch (e) {
				e.IsValid = false;

				if (this.isDebug == true) {
					alert(e);
				}
			}
		}
	},

	validate_compare: function(val, e, json) {
		e.CompareToValue = val.getAttribute("CompareToValue");
		e.Operator = val.getAttribute("CompareOperator");
		e.DateType = val.getAttribute("dataType");
		e.AlwaysTrueForNullValue = val.getAttribute("AlwaysTrueForNullValue");
		e.ValueToCompare = (json[e.CompareToValue] == null) ? "" : (json[e.CompareToValue] + "");

		if (e.AlwaysTrueForNullValue != null && e.AlwaysTrueForNullValue == "1" && (e.Value == null || e.value == "")) {
			e.IsValid = true;
			return;
		}

		var operator = e.Operator;
		var dateType = e.DateType;
		var value = e.Value;

		var valueToCompare = e.ValueToCompare;

		if (e.ValueToCompare == "1"
			&& (value == "" || valueToCompare == "" || null == value || null == valueToCompare)) {
			e.IsValid = true;
			return;
		}

		var valuesArray = e.ValueToCompare.split("||");
		for (var i = 0; i < valuesArray.length; i++) {
			if (e.IsValid == false)
				return;

			valueToCompare = valuesArray[i];

			switch (operator) {
				case "DataTypeCheck":
					switch (dateType) {
						case "integer":
							if (!IsInteger(value)) {
								e.IsValid = false;
							}
							break;
						case "double":
						case "decimal":
							if (!IsDouble(value)) {
								e.IsValid = false;
							}
							break;
						case "datetime":
							if (!value.isDateTime()) {
								e.IsValid = false;
							}
							break;
					}
					break;
				case "Equal":
					switch (dateType) {
						case "string":
							if (value != valueToCompare) {
								e.IsValid = false;
							}
							break;
						case "integer":
							if (!IsInteger(value)) {
								e.IsValid = false;
								return false;
							}
							if (parseInt(value) != parseInt(valueToCompare)) {
								e.IsValid = false;
							}
							break;
						case "double":
						case "decimal":
							if (!IsDouble(value)) {
								e.IsValid = false;
								return false;
							}
							if (parseFloat(value) != parseFloat(valueToCompare)) {
								e.IsValid = false;
							}
							break;
						case "datetime":
							var d = new Date(value);
							var c;
							if (!value.isDateTime()) {
								e.IsValid = false;
								return false;
							}
							c = new Date(valueToCompare);
							if (d != c) {
								e.IsValid = false;
							}
							break;
					}
					break;
				case "GreaterThan":
					switch (dateType) {
						case "string":
							if (value <= valueToCompare) {
								e.IsValid = false;
							}
							break;
						case "integer":
							if (!IsInteger(value)) {
								e.IsValid = false;
								return;
							}
							if (parseInt(value) <= parseInt(valueToCompare)) {
								e.IsValid = false;
							}
							break;
						case "double":
						case "decimal":
							if (!IsDouble(value)) {
								e.IsValid = false;
								return false;
							}
							if (parseFloat(value) <= parseFloat(valueToCompare)) {
								e.IsValid = false;
							}
							break;
						case "datetime":
							var d = new Date(value);
							var c;
							if (!value.isDateTime()) {
								e.IsValid = false;
								return false;
							}
							c = new Date(valueToCompare);
							if (d <= c) {
								e.IsValid = false;
							}
							break;
					}
					break;
				case "GreaterThanEqual":
					switch (dateType) {
						case "string":
							if (value < valueToCompare) {
								e.IsValid = false;
							}
							break;
						case "integer":
							if (!IsInteger(value)) {
								e.IsValid = false;
								return false;
							}
							if (parseInt(value) < parseInt(valueToCompare)) {
								e.IsValid = false;
							}
							break;
						case "double":
						case "decimal":
							if (!IsDouble(value)) {
								e.IsValid = false;
								return false;
							}
							if (parseFloat(value) < parseFloat(valueToCompare)) {
								e.IsValid = false;
							}
							break;
						case "datetime":
							var d = new Date(value);
							var c;
							if (!value.isDateTime()) {
								e.IsValid = false;
								return false;
							}
							c = new Date(valueToCompare);
							if (d < c) {
								e.IsValid = false;
							}
							break;
					}
					break;
				case "LessThan":
					switch (dateType) {
						case "string":
							if (value >= valueToCompare) {
								e.IsValid = false;
							}
							break;
						case "integer":
							if (!IsInteger(value)) {
								e.IsValid = false;
								return false;
							}
							if (parseInt(value) >= parseInt(valueToCompare)) {
								e.IsValid = false;
							}
							break;
						case "double":
						case "decimal":
							if (!IsDouble(value)) {
								e.IsValid = false;
								return false;
							}
							if (parseFloat(value) >= parseFloat(valueToCompare)) {
								e.IsValid = false;
							}
							break;
						case "datetime":
							var d = new Date(value);
							var c;
							if (!value.isDateTime()) {
								e.IsValid = false;
								return false;
							}
							c = new Date(valueToCompare);
							if (d >= c) {
								e.IsValid = false;
							}
							break;
					}
					break;
				case "LessThanEqual":
					switch (dateType) {
						case "string":
							if (value > valueToCompare) {
								e.IsValid = false;
							}
							break;
						case "integer":
							if (!IsInteger(value)) {
								e.IsValid = false;
								return false;
							}
							if (parseInt(value) > parseInt(valueToCompare)) {
								e.IsValid = false;
							}
							break;
						case "double":
						case "decimal":
							if (!IsDouble(value)) {
								e.IsValid = false;
								return false;
							}
							if (parseFloat(value) > parseFloat(valueToCompare)) {
								e.IsValid = false;
							}
							break;
						case "datetime":
							var d = new Date(value);
							var c;
							if (!value.isDateTime()) {
								e.IsValid = false;
								return false;
							}
							c = new Date(valueToCompare);
							if (d > c) {
								e.IsValid = false;
							}
							break;
					}
					break;
				case "NotEqual":
					switch (dateType) {
						case "string":
							if (value.toLowerCase().replace(/ /g,"") == valueToCompare) {
								e.IsValid = false;
							}
							break;
						case "integer":
							if (!IsInteger(value)) {
								e.IsValid = false;
								return false;
							}
							if (parseInt(value) == parseInt(valueToCompare)) {
								e.IsValid = false;
							}
							break;
						case "double":
						case "decimal":
							if (!IsDouble(value)) {
								e.IsValid = false;
								return false;
							}
							if (parseFloat(value) == parseFloat(valueToCompare)) {
								e.IsValid = false;
							}
							break;
						case "datetime":
							var d = new Date(value);
							var c;
							if (!value.isDateTime()) {
								e.IsValid = false;
								return false;
							}
							c = new Date(valueToCompare);
							if (d == c) {
								e.IsValid = false;
							}
							break;
					}
					break;
			}
		}

	},


	validate_textFilter: function(val, e) {
		e.MinLength = val.getAttribute("MinLenth");
		e.MaxLength = val.getAttribute("MaxLength");
		e.MinErrorMessage = val.getAttribute("MessageOfMinLength");
		e.MaxErrorMessage = val.getAttribute("MessageOfMaxLength");
		e.ValidateEmptyText = (val.getAttribute("ValidateEmptyText") == "1");

		if (!e.ValidateEmptyText && (null == e.Value || "" == e.Value)) {
			e.IsValid = true;
		}
		else {
			if (e.MinLength != null && e.MinLength != 0 && e.MinLength != '0') {
				e.IsValid = (e.Value != null && e.Value.length >= parseInt(e.MinLength));
				if (!e.IsValid) {
					e.ErrorMessage = e.MinErrorMessage.replace(/%%/g, e.MinLength);
				}
			}
			else if (e.MaxLength != null && e.MaxLength != 0 && e.MaxLength != '0') {
				e.IsValid = (e.Value != null && e.Value.length <= parseInt(e.MaxLength));
				if (!e.IsValid) {
					e.ErrorMessage = e.MaxErrorMessage.replace(/%%/g, e.MaxLength); ;
				}
			}
		}
	},

	add_onLoad: function(fn) {
		var arr = this.get_array(this.Event_OnLoad);
		arr.push(fn);
	},

	add_onValidate: function(fn) {
		var arr = this.get_array(this.Event_OnValidate);
		arr.push(fn);
	},

	add_onError: function(fn) {
		var arr = this.get_array(this.Event_OnError);
		arr.push(fn);
	},

	add_onSuccess: function(fn) {
		var arr = this.get_array(this.Event_OnSuccess);
		arr.push(fn);
	},

	add_onBeforeValidate: function(fn) {
		var arr = this.get_array(this.Event_OnBeforeValidate);
		arr.push(fn);
	},

	get_array: function(eType) {
		if (this.eventsMapping[eType] == null) {
			this.eventsMapping[eType] = [];
		}

		return this.eventsMapping[eType];
	},

	fire: function(eType, sender, args) {
		var events = this.eventsMapping[eType];

		if (events != null) {
			for (var i = 0; i < events.length; i++) {
				try {
					var fn = events[i];

					if (typeof (fn) == "string") {
						eval(fn + "(sender, args)");
					}
					else {
						fn.call(null, sender, args);
					}
				}
				catch (e) {
					if (this.isDebug == true) {
						alert(e);
					}
				}
			}
		}
	}
}