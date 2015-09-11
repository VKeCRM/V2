String.prototype.startsWith = function(s) {
	if (s.length > this.length)
		return false;
	else if (s.length == this.length)
		return s == this;

	return (this.substring(0, s.length) == s);
}

// VKeCRMSys Constructor
function VKeCRMSys()
{
	// hold valId at submit button
	this.GroupFlag = "MvcValGroup";
	this.eventsMapping = {};
	// hold all mvc validators
	this.pageValidators = [];
	this.validatorMapping = {};
	
	// hold all tabs
	this.pageTabs = [];
	this.tabMapping = {};

	this.CustomerEvents = {
		OnPreHandleError: 'OnPreHandleError',
		OnHandleError: 'OnHandleError'
	};

	this.ErrorType =
	{
		None: 0,
		Normal: 1,

		AuthenticationFailed: 2,
		TokenValidationgFailed: 3,
		SessionKickOutException: 4,

		ValidationFailedAtClient: 998,
		ValidationFailedAtServer: 999,
		//BusinessValidationFailed: 1000,
		BusinessLevalException: 1001,
		JsonDeserializeError: 1002,
		PermissionDenied: 1003,

		Undefined: 9999
	};
}

// VKeCRMSys static methods
VKeCRMSys.initialize = function() { VKeCRMSys._instance = new VKeCRMSys(); }

VKeCRMSys.getInstance = function() { return VKeCRMSys._instance; };

// VKeCRMSys Prototype
VKeCRMSys.prototype =
{
	page_load: function()
	{
		for (var i = 0; i < this.pageValidators.length; i++)
		{
			this.pageValidators[i].validation_load();
			this.pageValidators[i].add_onError("VKeCRMSys.getInstance().handle_error");
		}
		
		for (var i = 0; i < this.pageTabs.length; i++)
		{
			this.pageTabs[i].tabStrip_load();
		}
	},
	
	add_tabStrip: function(tabStripId, multiPageId, opts){
		var tabStrip = new VKeCRMTabStrip(tabStripId, multiPageId, opts);
		this.pageTabs.push(tabStrip);
		this.tabMapping[tabStripId] = tabStrip;
	},
	
	get_tabStrip : function(tabStripId)
	{
		var tabStrip = this.tabMapping[tabStripId];
		if (null == tabStrip)
			throw "Cannot find tabStrip whose Id: [" + tabStripId + "]";
		else
			return tabStrip;
	},

	// begin handle mvc validation
	add_pageValidator: function(valId)
	{
		var mvcVal = new VKeCRMMvcValidation(valId);
		this.pageValidators.push(mvcVal);
		this.validatorMapping[valId] = mvcVal;
	},

	get_validator: function(validatorId)
	{
		var validator = this.validatorMapping[validatorId];
		if (null == validator)
			throw "Cannot find validator whose Id: [" + validatorId + "]";
		else
			return validator;
	},
	
	application_submit: function(event)
	{
		try 
		{
			var element = event.target ? event.target : event.srcElement;
			var valId = element.getAttribute(VKeCRMSys.getInstance().GroupFlag);
			var result = VKeCRMSys.getInstance().get_validator(valId).validate(); 
		}
		catch (e)
		{ 
			alert(e); 
		}
	},

	register_validation: function(buttonId, valId)
	{
		var btn = document.getElementById(buttonId);
		btn.setAttribute(this.GroupFlag, valId);

		if (btn.addEventListener)
			btn.addEventListener('click', VKeCRMSys.getInstance().application_submit, false);
		else if (btn.attachEvent)
			btn.attachEvent("onclick", VKeCRMSys.getInstance().application_submit);
		else
			btn["onclick"] = VKeCRMSys.getInstance().application_submit;
	},
	// == end

	// page level error handle
	add_onPreHandleError: function(fn)
	{
		this.get_events(this.CustomerEvents.OnPreHandleError).push(fn);
	},

	add_onHandleError: function(fn)
	{
		this.get_events(this.CustomerEvents.OnHandleError).push(fn);
	},

	// this method will be called by framework
	// when error occurs
	handle_error: function(sender, e)
	{
		e.Handled = false;

		this.fire(this.CustomerEvents.OnPreHandleError, sender, e);

		this.fire(this.CustomerEvents.OnHandleError, sender, e);

		// if no customer handler(s) or e.Handled == false,
		// will call default error handler.
		if (e.Handled === false)
			this.default_errorHandle(sender, e);
	},

	default_errorHandle: function(sender, e)
	{
		var errorType = sender || (e && e.Error) || this.ErrorType.Undefined;

		switch (errorType)
		{
			// goes to signin page if AuthenticationFailed         
			case this.ErrorType.AuthenticationFailed:
				var loginUrl = e.LoginUrl || '/signin.aspx';

				var returnUrl = '';
				if (loginUrl.toLowerCase().startsWith('http'))
					returnUrl = document.location.href;
				else
					returnUrl = document.location.pathname + document.location.search;

				var fullUrl = loginUrl + "?ReturnUrl=" + encodeURIComponent(returnUrl);
				document.location.replace(fullUrl);
				break;
				
			case this.ErrorType.SessionKickOutException:
				var loginUrl = (e.LoginUrl || '/signin.aspx') + '?reasoncode=' + e.DataSource;
				document.location.replace(loginUrl);
				break;

			// normally show page level error        
			case this.ErrorType.Normal:
			case this.ErrorType.BusinessLevalException:
			case this.ErrorType.TokenValidationgFailed:
			case this.ErrorType.JsonDeserializeError:
				//case this.ErrorType.BusinessValidationFailed:
				var target = document.getElementById(e.ShowErrorAt);
				if (null == target)
				{
					//alert("We got an error here: " + "{ Error: " + errorType + ", ErrorMessage: " + e.ErrorMessage + "}\r\n" + "Please add VKeCRMSys.getInstance().add_onPreHandleError(fn) to handle it. (Set e.ShowErrorAt in your method)");
				}
				else
				{
					target.innerHTML = e.ErrorMessage;
					target.style.display = "block";
				}
				break;

			case this.ErrorType.PermissionDenied:
				document.location.replace(e.DataSource.Where2Go);
				break;

			// show inline or (and) summary        
			case this.ErrorType.ValidationFailedAtClient:
			case this.ErrorType.ValidationFailedAtServer:
				if (e.ShowSummary === true)
				{
					var target = document.getElementById(e.ShowErrorAt);
					if (null != target)
					{
						var errorHTML = "<div class='message-summary'><b>" + e.Title + "</b>";
						errorHTML += "<ul>";
						for (var i = 0; i < e.DataSource.length; i++)
						{
							errorHTML += "<li>" + e.DataSource[i].ErrorMessage + '</li>';
						}
						errorHTML += "</ul></div>";

						target.innerHTML = errorHTML;
						target.style.display = "block";
					}
				}
				break;

			// just alert        
			case this.ErrorType.Undefined:
				//alert(e.ErrorMessage);
				break;
		}
		
		if(e.stopAtTop == true)
			window.scrollTo(0, 0);
	},
	// == end

	// common methods
	get_events: function(eType)
	{
		if (!this.eventsMapping[eType])
			this.eventsMapping[eType] = [];

		return this.eventsMapping[eType];
	},

	fire: function(eType, sender, args)
	{
		var arr = this.get_events(eType);

		for (var i = 0; i < arr.length; i++)
		{
			try
			{
				arr[i].call(null, sender, args);
			}
			catch (e)
			{
				if (this.isDebug == true)
					alert(e);
			}
		}
	}
	// == end
}
