// Register the namespace for the control.
Type.registerNamespace('VKeCRM.Framework.Web.UI.Controls.ControlExtender');

VKeCRM.Framework.Web.UI.Controls.ControlExtender.TransferCalendarExtender = function(element) {
	/// <summary>
	/// A behavior that attaches a calendar date selector to a textbox
	/// </summmary>
	/// <param name="element" type="Sys.UI.DomElement">The element to attach to</param>

	VKeCRM.Framework.Web.UI.Controls.ControlExtender.TransferCalendarExtender.initializeBase(this, [element]);

	this._apesonHolidays = null;
	this._aserverOffset = null;
	this._lowerBoundDate = null;
	this._upperBoundDate = null;
	this._customerTodayDateForVKeCRM = null;
	this.GMTTodayDate = "Wed, 10 Feb 2010 00:00:00 GMT";  //only is default value,can set others
}

VKeCRM.Framework.Web.UI.Controls.ControlExtender.TransferCalendarExtender.prototype = {
	initialize: function() {
		/// <summary>
		/// Initializes the components and parameters for this behavior
		/// </summary>

		VKeCRM.Framework.Web.UI.Controls.ControlExtender.TransferCalendarExtender.callBaseMethod(this, "initialize");
	},
	dispose: function() {
		/// <summary>
		/// Disposes this behavior's resources
		/// </summary>

		VKeCRM.Framework.Web.UI.Controls.ControlExtender.TransferCalendarExtender.callBaseMethod(this, "dispose");
	},


	get_apesonHolidays: function() {
		/// <summary>
		/// timeZone
		/// </summary>
		/// <value type="Int" />
		if (this._apesonHolidays != null) {
			return this._apesonHolidays;
		}
	},
	set_apesonHolidays: function(value) {
		if (value && (String.isInstanceOfType(value)) && (value.length != 0)) {
			value = value.split(",");
		}
		if (this._apesonHolidays != value) {
			this._apesonHolidays = value;
			this.raisePropertyChanged("apesonHolidays");
		}
	},

	get_aserverOffset: function() {
		/// <summary>
		/// timeZone
		/// </summary>
		/// <value type="Int" />
		if (this._aserverOffset != null) {
			return this._aserverOffset;
		}
	},
	set_aserverOffset: function(value) {
		if (this._aserverOffset != value) {
			this._aserverOffset = value;
			this.raisePropertyChanged("aserverOffset");
		}
	},

	changeToESTime: function(value, offset) {
		value = new Date(value);

		var localTime = value.getTime();
		var localOffset = value.getTimezoneOffset() * 60000;
		var utc = localTime + localOffset;
		var est = utc + (3600000 * offset);
		value = new Date(est);
		return value;
	},

	getClientTimeZone: function(value) {
		var munites = value.getTimezoneOffset();
		var hour = parseInt(munites / 60);
		var munite = munites % 60;
		var prefix = "-";
		if (hour < 0 || munite < 0) {
			prefix = "+";
			hour = -hour;
			if (munite < 0) {
				munite = -munite;
			}
		}
		hour = hour + "";
		munite = munite + "";
		if (hour.length == 1) {
			hour = "0" + hour;
		}
		if (munite.length == 1) {
			munite = "0" + munite;
		}
		return prefix + hour + munite;
	},

	get_lowerBoundDate: function() {
		/// <summary>
		/// The date currently visible in the calendar
		/// </summary>
		/// <value type="Date" />
		if (this._lowerBoundDate != null) {
			return this._lowerBoundDate;
		}
	},
	set_lowerBoundDate: function(value) {
		if (value && (String.isInstanceOfType(value)) && (value.length != 0)) {
			value = new Date(value);
		}
		//if (value) value = value.getDateOnly();
		if (this._lowerBoundDate != value) {
			var offset = -5
			if (this._aserverOffset != null)
				offset = this.get_aserverOffset();
			this._lowerBoundDate = this.changeToESTime(value, offset);
			this.raisePropertyChanged("lowerBoundDate");
		}
	},

	get_upperBoundDate: function() {
		if (this._upperBoundDate != null) {
			return this._upperBoundDate;
		}
	},
	set_upperBoundDate: function(value) {
		if (value && (String.isInstanceOfType(value)) && (value.length != 0)) {
			value = new Date(value);
		}
		//if (value) value = value.getDateOnly();
		if (this._upperBoundDate != value) {
			var offset = -5
			if (this._aserverOffset != null)
				offset = this.get_aserverOffset();
			this._upperBoundDate = this.changeToESTime(value, offset);
			this.raisePropertyChanged("upperBoundDate");
		}
	},

	get_customerTodayDateForVKeCRM: function() {
		/// <value type="Date">
		/// The date to use for "Today"
		/// </value>
		if (this._customerTodayDateForVKeCRM != null) {
			return this._customerTodayDateForVKeCRM;
		}
	},

	set_customerTodayDateForVKeCRM: function(value) {
		if (value && (String.isInstanceOfType(value)) && (value.length != 0)) {
			this.GMTTodayDate = value;
			value = new Date(value);
		}
		//if (value) value = value.getDateOnly();
		if (this._customerTodayDateForVKeCRM != value) {
			var offset = -5
			if (this._aserverOffset != null)
				offset = this.get_aserverOffset();
			this._customerTodayDateForVKeCRM = this.changeToESTime(value, offset);
			this.raisePropertyChanged("customerTodayDateForVKeCRM");
		}
	},

	getTodaysDate: function() {
		if (this._customerTodayDateForVKeCRM == null) {
			var localTime = d.getTime();
			var localOffset = d.getTimezoneOffset() * 60000;
			var utc = localTime + localOffset;
			var offset = -5;
			var est = utc + (3600000 * offset);
			var esTime = new Date(est);
			var todaysDate = esTime;
		}
		else {
			todaysDate = this._customerTodayDateForVKeCRM;
		}
		return todaysDate;

	},

	zcheckBusinessDate: function(value) {
		//if value is weekend return false
		var dzhou = value.getDay();
		//if todayDateForVKeCRM is GMT:5AM-10PM return false:millisecond
		var GMTTodayDateList = this.GMTTodayDate.split(" ");
		var GMTTodayTimeList = GMTTodayDateList[4].split(":");
		var businessTimeForToday = GMTTodayTimeList[0] * 3600000 + GMTTodayTimeList[1] * 60000 + GMTTodayTimeList[2] * 1000;

		//penson holiday day
		if (this._apesonHolidays != null) {
			for (var i = 0; i < this._apesonHolidays.length; i++) {
				if (this._apesonHolidays[i] == (value.getFullYear() + "-" + (value.getMonth() + 1) + "-" + value.getDate())) {
					return true;
				}
			}
		}

		//others
		if ((dzhou == 6) || (dzhou == 0)) {
			return true;
		}
		else if ((businessTimeForToday < 18000000 || businessTimeForToday > 79200000) && value.toDateString() == this.getTodaysDate().toDateString()) {
			return true;
		}

		else {
			return false;
		}
	},

	_performLayout: function() {
		/// <summmary>
		/// Updates the various views of the calendar to match the current selected and visible dates
		/// </summary>

		var elt = this.get_element();
		if (!elt) return;
		if (!this.get_isInitialized()) return;
		if (!this._isOpen) return;

		var dtf = Sys.CultureInfo.CurrentCulture.dateTimeFormat;
		var selectedDate = this.get_selectedDate();
		var visibleDate = this._getEffectiveVisibleDate();
		//calculate est time
		var d = new Date();

		var todaysDate = this.getTodaysDate();


		switch (this._mode) {
			case "days":

				var firstDayOfWeek = this._getFirstDayOfWeek();
				var daysToBacktrack = visibleDate.getDay() - firstDayOfWeek;
				if (daysToBacktrack <= 0)
					daysToBacktrack += 7;

				var startDate = new Date(visibleDate.getFullYear(), visibleDate.getMonth(), visibleDate.getDate() - daysToBacktrack, this._hourOffsetForDst);
				var currentDate = new Date(startDate.toDateString() + " 23:59:59 GMT" + this.getClientTimeZone(startDate));

				for (var i = 0; i < 7; i++) {
					var dayCell = this._daysTableHeaderRow.cells[i].firstChild;
					if (dayCell.firstChild) {
						dayCell.removeChild(dayCell.firstChild);
					}
					dayCell.appendChild(document.createTextNode(dtf.ShortestDayNames[(i + firstDayOfWeek) % 7]));
				}
				for (var week = 0; week < 6; week++) {
					var weekRow = this._daysBody.rows[week];
					for (var dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++) {
						var dayCell = weekRow.cells[dayOfWeek].firstChild;
						if (dayCell.firstChild) {
							dayCell.removeChild(dayCell.firstChild);
						}
						dayCell.appendChild(document.createTextNode(currentDate.getDate()));
						dayCell.title = currentDate.localeFormat("D");
						dayCell.date = currentDate;

						//modify by Tommy 2009.2.9
						$common.removeCssClasses(dayCell.parentNode, ["ajax__calendar_other", "ajax__calendar_disabled", "ajax__calendar_active", "custom__calendar_lower"]);

						if (this._lowerBoundDate == null && this._upperBoundDate == null) {
							Sys.UI.DomElement.addCssClass(dayCell.parentNode, this._getCssClass(dayCell.date, 'd'));
						}
						else if ((this._upperBoundDate != null && currentDate > this._upperBoundDate) ||
                         (this._lowerBoundDate != null && currentDate < this._lowerBoundDate) ||
                         (this.zcheckBusinessDate(currentDate))) {
							Sys.UI.DomElement.addCssClass(dayCell.parentNode, "custom__calendar_lower");
						}

						//modify end

						currentDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate() + 1, this._hourOffsetForDst);
						currentDate = new Date(currentDate.toDateString() + " 23:59:59 GMT" + this.getClientTimeZone(currentDate));
					}
				}

				this._prevArrow.date = new Date(visibleDate.getFullYear(), visibleDate.getMonth() - 1, 1, this._hourOffsetForDst);
				this._nextArrow.date = new Date(visibleDate.getFullYear(), visibleDate.getMonth() + 1, 1, this._hourOffsetForDst);
				if (this._title.firstChild) {
					this._title.removeChild(this._title.firstChild);
				}
				this._title.appendChild(document.createTextNode(visibleDate.localeFormat("MMMM, yyyy")));
				this._title.date = visibleDate;

				break;
			case "months":

				for (var i = 0; i < this._monthsBody.rows.length; i++) {
					var row = this._monthsBody.rows[i];
					for (var j = 0; j < row.cells.length; j++) {
						var cell = row.cells[j].firstChild;
						cell.date = new Date(visibleDate.getFullYear(), cell.month, 1, this._hourOffsetForDst);
						cell.title = cell.date.localeFormat("Y");
						$common.removeCssClasses(cell.parentNode, ["ajax__calendar_other", "ajax__calendar_disabled", "ajax__calendar_active"]);
						Sys.UI.DomElement.addCssClass(cell.parentNode, this._getCssClass(cell.date, 'M'));
					}
				}

				if (this._title.firstChild) {
					this._title.removeChild(this._title.firstChild);
				}
				this._title.appendChild(document.createTextNode(visibleDate.localeFormat("yyyy")));
				this._title.date = visibleDate;
				this._prevArrow.date = new Date(visibleDate.getFullYear() - 1, 0, 1, this._hourOffsetForDst);
				this._nextArrow.date = new Date(visibleDate.getFullYear() + 1, 0, 1, this._hourOffsetForDst);

				break;
			case "years":

				var minYear = (Math.floor(visibleDate.getFullYear() / 10) * 10);
				for (var i = 0; i < this._yearsBody.rows.length; i++) {
					var row = this._yearsBody.rows[i];
					for (var j = 0; j < row.cells.length; j++) {
						var cell = row.cells[j].firstChild;
						cell.date = new Date(minYear + cell.year, 0, 1, this._hourOffsetForDst);
						if (cell.firstChild) {
							cell.removeChild(cell.lastChild);
						} else {
							cell.appendChild(document.createElement("br"));
						}
						cell.appendChild(document.createTextNode(minYear + cell.year));
						$common.removeCssClasses(cell.parentNode, ["ajax__calendar_other", "ajax__calendar_disabled", "ajax__calendar_active"]);
						Sys.UI.DomElement.addCssClass(cell.parentNode, this._getCssClass(cell.date, 'y'));
					}
				}

				if (this._title.firstChild) {
					this._title.removeChild(this._title.firstChild);
				}
				this._title.appendChild(document.createTextNode(minYear.toString() + "-" + (minYear + 9).toString()));
				this._title.date = visibleDate;
				this._prevArrow.date = new Date(minYear - 10, 0, 1, this._hourOffsetForDst);
				this._nextArrow.date = new Date(minYear + 10, 0, 1, this._hourOffsetForDst);

				break;
		}
		if (this._today.firstChild) {
			this._today.removeChild(this._today.firstChild);
		}
		this._today.appendChild(document.createTextNode(String.format(AjaxControlToolkit.Resources.Calendar_Today, todaysDate.localeFormat("MMMM dd, yyyy"))));
		this._today.date = todaysDate;
	},

	_isOutOfBounds: function(date, part) {
		/// <summary>
		/// Gets whether the supplied date is out of the lower / upper bound date limits
		/// </summary>
		/// <param name="date" type="Date">The date to match</param>
		/// <param name="part" type="String">The most significant part of the date to test</param>
		/// <returns type="Boolean" />
		switch (part) {
			case 'd':
				//modify by Tommy 2009.2.9
				if (this._lowerBoundDate && date < this._lowerBoundDate) {
					return true;
				}
				else if (this._upperBoundDate && date > this._upperBoundDate) {
					return true;
				}
				else if (this.zcheckBusinessDate(date)) {
					return true;
				}
				break;
			case 'M':
				//modify by Tommy 2009.2.9
				if (this._lowerBoundDate && ((date.getMonth() < this._lowerBoundDate.getMonth() &
                       date.getFullYear() == this._lowerBoundDate.getFullYear()) || date.getFullYear() < this._lowerBoundDate.getFullYear())) {
					return true;
				}
				else if (this._upperBoundDate &&
                ((date.getFullYear() == this._upperBoundDate.getFullYear() && date.getMonth() > this._upperBoundDate.getMonth()) ||
                       date.getFullYear() > this._upperBoundDate.getFullYear())) {
					return true;
				}
				break;
			case 'y':
				if (this._lowerBoundDate && date.getFullYear() < this._lowerBoundDate.getFullYear())
					return true;
				else if (this._upperBoundDate && date.getFullYear() > this._upperBoundDate.getFullYear())
					return true;
				break;
		}
		return false;
	},
	_getCssClass: function(date, part) {
		/// <summary>
		/// Gets the cssClass to apply to a cell based on a supplied date
		/// </summary>
		/// <param name="date" type="Date">The date to match</param>
		/// <param name="part" type="String">The most significant part of the date to test</param>
		/// <returns type="String" />

		if (this._isSelected(date, part)) {
			return "ajax__calendar_active";
		} else if (this._isOutOfBounds(date, part)) {
			return "ajax__calendar_disabled";
		} else if (this._isOther(date, part)) {
			return "ajax__calendar_other";
		} else {
			return "";
		}
	},
	_cell_onclick: function(e) {
		/// <summary> 
		/// Handles the click event of a cell
		/// </summary>
		/// <param name="e" type="Sys.UI.DomEvent">The arguments for the event</param>

		e.stopPropagation();
		e.preventDefault();

		if (!this._enabled) return;

		var target = e.target;
		var visibleDate = this._getEffectiveVisibleDate();
		Sys.UI.DomElement.removeCssClass(target.parentNode, "ajax__calendar_hover");
		switch (target.mode) {
			case "prev":
			case "next":
				this._switchMonth(target.date);
				break;
			case "title":
				switch (this._mode) {
					case "days": this._switchMode("months"); break;
					case "months": this._switchMode("years"); break;
				}
				break;
			case "month":

				if (!this._isOutOfBounds(target.date, 'M')) {
					if (target.month == visibleDate.getMonth()) {
						this._switchMode("days");
					} else {
						this._visibleDate = target.date;
						this._switchMode("days");
					}
				}
				break;
			case "year":
				if (!this._isOutOfBounds(target.date, 'y')) {
					if (target.date.getFullYear() == visibleDate.getFullYear()) {
						this._switchMode("months");
					} else {
						this._visibleDate = target.date;
						this._switchMode("months");
					}
				}
				break;
			case "day":
				if (!this._isOutOfBounds(target.date, 'd')) {
					this.set_selectedDate(target.date);
					this._switchMonth(target.date);
					this._blur.post(true);
					this.raiseDateSelectionChanged();
				}
				break;
			case "today":
				if (!this._isOutOfBounds(target.date, 'd')) {
					this.set_selectedDate(target.date);
					this._switchMonth(target.date);
					this._blur.post(true);
					this.raiseDateSelectionChanged();
				}
				break;
		}
	}
}

VKeCRM.Framework.Web.UI.Controls.ControlExtender.TransferCalendarExtender.registerClass("VKeCRM.Framework.Web.UI.Controls.ControlExtender.TransferCalendarExtender", AjaxControlToolkit.CalendarBehavior);

