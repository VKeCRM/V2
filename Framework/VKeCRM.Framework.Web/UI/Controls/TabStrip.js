
function VKeCRMTabStrip(tabId, viewId, opts) {
	this.tabs = $('#' + tabId + ' .rtsLink');
	this.views = $('#' + viewId + ' .rtvDIV');

	if (this.tabs.length != this.views.length)
		throw 'tabs length does not equal views lenth: ' + this.tabs.length + ' vs ' + this.views.length;

	this.currentTabIndex = opts ? opts.currentTabIndex : 0;

	this.eventsMapping = {};
}

VKeCRMTabStrip.prototype =
{
	tabStrip_load: function() {
		var instance = this;
		this.tabs.each(function(index) {

			var tab = $(this);
			var currentIndex = index;

			tab.bind('click', function(e) {

				var previousIndex = instance.currentTabIndex;
				if (previousIndex == currentIndex)
					return;

				// hide previous tab & view
				var selectedTab = instance.tabs[previousIndex];
				$(selectedTab).removeClass('rtsSelected');
				var selectedView = instance.views[previousIndex];
				$(selectedView).hide();

				// show current tab & view
				tab.addClass('rtsSelected');
				var reletedView = instance.views[currentIndex];
				$(reletedView).show();

				// set index
				instance.currentTabIndex = currentIndex;

				// fire events
				var arg = new Object();
				arg.view = {
					isVisible: $(reletedView).hasClass('rtsHidden'),
					index: currentIndex
				};
				instance.fire('TabChanged', instance, arg);
			});
		});
	},

	set_selectedIndex: function(index) {
		var instance = this;
		var previousIndex = instance.currentTabIndex;

		// hide previous tab & view
		var selectedTab = instance.tabs[previousIndex];
		$(selectedTab).removeClass('rtsSelected');
		var selectedView = instance.views[previousIndex];
		$(selectedView).hide();

		// show current tab & view
		var tab = instance.tabs[index];
		$(tab).addClass('rtsSelected');
		var reletedView = instance.views[index];
		$(reletedView).show();

		// set index
		instance.currentTabIndex = index;

		// fire events
		var arg = new Object();
		arg.view = {
			isVisible: $(reletedView).hasClass('rtsHidden'),
			index: currentIndex
		};
		instance.fire('TabChanged', instance, arg);

		return index;
    },

    set_hidden: function(index) {
        var instance = this;
        var tab = instance.tabs[index];
        $(tab).addClass('hidden');
        var reletedView = instance.views[index];
        $(reletedView).addClass('rtsHidden');
    },

	opposite_view: function() {
		var instance = this;
		var index = instance.currentTabIndex;
		var selectedView = instance.views[index];

		var theView = $(selectedView);

		if (theView.hasClass('rtsHidden')) {
			theView.removeClass('rtsHidden');
			return 1;
		}
		else {
			theView.addClass('rtsHidden');
			return -1;
		}
	},

	show_view: function(viewId) {
		var instance = this;
		var selectedView = instance.views[viewId];
		var theView = $(selectedView);
		theView.removeClass('rtsHidden');
	},

	hide_view: function(viewId) {
		var instance = this;
		var selectedView = instance.views[viewId];
		var theView = $(selectedView);
		theView.addClass('rtsHidden');
	},

	add_onTabChanged: function(fn) {
		var arr = this.get_events('TabChanged');
		arr.push(fn);
	},

	get_events: function(eType) {
		if (!this.eventsMapping[eType])
			this.eventsMapping[eType] = [];

		return this.eventsMapping[eType];
	},

	fire: function(eType, sender, args) {
		var arr = this.get_events(eType);

		for (var i = 0; i < arr.length; i++) {
			try {
				arr[i].call(null, sender, args);
			}
			catch (e) {
				alert(e)
			}
		}
	}
}


