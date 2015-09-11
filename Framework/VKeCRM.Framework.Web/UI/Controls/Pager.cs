using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VKeCRM.Framework.Web.UI.Controls
{
    [ToolboxData("<{0}:Pager runat=\"server\"></{0}:Pager>")]
    public class Pager : WebControl, IPostBackEventHandler, INamingContainer
    {
        #region Save/Load Control State
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }

        protected override object SaveControlState()
        {
            object[] objState = new object[3];
            objState[0] = CurrentIndex;
            objState[1] = PageSize;
            objState[2] = VirtualItemCount;
            return objState;
        }

        protected override void LoadControlState(object state)
        {
            object[] savedState = (object[])state;
            CurrentIndex = (int)savedState[0];
            PageSize = (int)savedState[1];
            VirtualItemCount = (double)savedState[2];
        }
        #endregion

        #region PostBack Stuff
        private static readonly object EventPageIndexChanged = new object();

        public event CommandEventHandler PageIndexChanged
        {
            add { Events.AddHandler(EventPageIndexChanged, value); }
            remove { Events.RemoveHandler(EventPageIndexChanged, value); }
        }

        protected virtual void OnPageIndexChanged(CommandEventArgs e)
        {
            CommandEventHandler clickHandler = (CommandEventHandler)Events[EventPageIndexChanged];
            if (clickHandler != null) clickHandler(this, e);
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            OnPageIndexChanged(new CommandEventArgs(this.UniqueID, Convert.ToInt32(eventArgument)));
        }
        #endregion

        #region Accessors (Behavioural)

        /// <summary>
        /// Gets or sets total number of rows.
        /// </summary>
        private double _virtualItemCount;
        [Browsable(false)]
        public double VirtualItemCount
        {
            get { return _virtualItemCount; }
            set
            {
                _virtualItemCount = value;

                double divide = VirtualItemCount / PageSize;
                double ceiled = System.Math.Ceiling(divide);
                PageCount = Convert.ToInt32(ceiled);
            }
        }

        /// <summary>
        /// Gets or sets current page index.
        /// </summary>
        private int _currentIndex = 1;
        [Browsable(false)]
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set { _currentIndex = value; }
        }

        /// <summary>
        /// Gets or sets page size (results per page).
        /// </summary>
        private int _pageSize = 15;
        [Category("Behavioural")]
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        private int _pageCount;
        [Browsable(false)]
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        /// <summary>
        /// Gets or sets the value that indicates whether the Next and Last clause is rendered as UI on page.
        /// </summary>
        private bool _showFirstLast = true;
        [Category("Behavioural")]
        public bool GenerateFirstLastSection
        {
            get { return _showFirstLast; }
            set { _showFirstLast = value; }
        }

        /// <summary>
        /// Gets or sets the value that indicates whether the SmartShortcuts are rendered as UI on page.
        /// </summary>
        private bool _enableSSC = false;
        [Category("Behavioural")]
        public bool GenerateSmartShortCuts
        {
            get { return _enableSSC; }
            set { _enableSSC = value; }
        }

        /// <summary>
        /// Gets or sets the value that will be used to calculate SmartShortcuts.
        /// </summary>
        private double _sscRatio = 3.0D;
        [Category("Behavioural")]
        public double SmartShortCutRatio
        {
            get { return _sscRatio; }
            set { _sscRatio = value; }
        }

        /// <summary>
        /// Gets or sets maximum number of SmartShortcuts that can be rendered.
        /// </summary>
        private int _maxSmartShortCutCount = 6;
        [Category("Behavioural")]
        public int MaxSmartShortCutCount
        {
            get { return _maxSmartShortCutCount; }
            set { _maxSmartShortCutCount = value; }
        }

        /// <summary>
        /// Gets or sets a value that to have the SmartShortcuts rendered, the page count must be greater that this value.
        /// </summary>
        private int _sscThreshold = 50;
        [Category("Behavioural")]
        public int SmartShortCutThreshold
        {
            get { return _sscThreshold; }
            set { _sscThreshold = value; }
        }

        /// <summary>
        /// Gets or sets the number of rendered page numbers in compact mode.
        /// </summary>
        private int _firstCompactedPageCount = 10;
        [Category("Behavioural")]
        public int CompactModePageCount
        {
            get { return _firstCompactedPageCount; }
            set { _firstCompactedPageCount = value; }
        }

        /// <summary>
        /// Gets or sets the number of rendered page numbers in standard mode.
        /// </summary>
        private int _notCompactedPageCount = 15;
        [Category("Behavioural")]
        public int NormalModePageCount
        {
            get { return _notCompactedPageCount; }
            set { _notCompactedPageCount = value; }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether Pager renders Alt tooltip.
        /// </summary>
        private bool _altEnabled = true;
        [Category("Behavioural")]
        public bool GenerateToolTips
        {
            get { return _altEnabled; }
            set { _altEnabled = value; }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether Pager information cell is rendered.
        /// </summary>
        private bool _infoCellVisible = true;
        [Category("Behavioural")]
        public bool GeneratePagerInfoSection
        {
            get { return _infoCellVisible; }
            set { _infoCellVisible = value; }
        }

        //// LLD: disable this feature
        ///// <summary>
        ///// Gets or sets a value that indicats whether GoTo section is rendered.
        ///// </summary>
        //private bool _generateGoToSection = false;
        //[Category("Behavioural")]
        //public bool GenerateGoToSection
        //{
        //    get { return _generateGoToSection; }
        //    set { _generateGoToSection = value; }
        //}

        /// <summary>
        /// Gets or sets a value that indicates whether hidden hyperlinks should render.
        /// </summary>
        private bool _generateHiddenHyperlinks = false;
        [Category("Behavioural")]
        public bool GenerateHiddenHyperlinks
        {
            get { return _generateHiddenHyperlinks; }
            set { _generateHiddenHyperlinks = value; }
        }

        /// <summary>
        /// Gets or sets the hidden hyperlinks' QueryString parameter name.
        /// </summary>
        private string _queryStringParameterName = "pagerControlCurrentPageIndex";
        [Category("Behavioural")]
        public string QueryStringParameterName
        {
            get { return _queryStringParameterName; }
            set { _queryStringParameterName = value; }
        }

        #endregion

        #region // Accessors (Globalization)

        /// <summary>
        /// Gets or sets the text caption displayed as "go" in the pager control.
        /// Default value: go
        /// </summary>
        private string _GO = "go";
        [Category("Globalization")]
        public string GoClause
        {
            get { return _GO; }
            set { _GO = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "of" in the pager control.
        /// Default value: of
        /// </summary>
        private string _OF = "of";
        [Category("Globalization")]
        public string OfClause
        {
            get { return _OF; }
            set { _OF = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "from" in the pager control.
        /// Default value: From
        /// </summary>
        private string _FROM = "From";
        [Category("Globalization")]
        public string FromClause
        {
            get { return _FROM; }
            set { _FROM = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "page" in the pager control.
        /// Default value: Page
        /// </summary>
        private string _PAGE = "Page";
        [Category("Globalization")]
        public string PageClause
        {
            get { return _PAGE; }
            set { _PAGE = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "to" in the pager control.
        /// Default value: to
        /// </summary>
        private string _TO = "to";
        [Category("Globalization")]
        public string ToClause
        {
            get { return _TO; }
            set { _TO = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "Showing Results" in the pager control.
        /// Default value: Showing Results
        /// </summary>
        private string _SHOWING_RESULT = "Showing Results";
        [Category("Globalization")]
        public string ShowingResultClause
        {
            get { return _SHOWING_RESULT; }
            set { _SHOWING_RESULT = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "Show Result" in the pager control.
        /// Default value: Show Result
        /// </summary>
        private string _SHOW_RESULT = "Show Result";
        [Category("Globalization")]
        public string ShowResultClause
        {
            get { return _SHOW_RESULT; }
            set { _SHOW_RESULT = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "to First Page" in the pager control.
        /// Default value: to First Page
        /// </summary>
        private string _BACK_TO_FIRST = "to First Page";
        [Category("Globalization")]
        public string BackToFirstClause
        {
            get { return _BACK_TO_FIRST; }
            set { _BACK_TO_FIRST = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "to Last Page" in the pager control.
        /// Default value: to Last Page
        /// </summary>
        private string _GO_TO_LAST = "to Last Page";
        [Category("Globalization")]
        public string GoToLastClause
        {
            get { return _GO_TO_LAST; }
            set { _GO_TO_LAST = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "Back to Page" in the pager control.
        /// Default value: Back to Page
        /// </summary>
        private string _BACK_TO_PAGE = "Back to Page";
        [Category("Globalization")]
        public string BackToPageClause
        {
            get { return _BACK_TO_PAGE; }
            set { _BACK_TO_PAGE = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "Next to Page" in the pager control.
        /// Default value: Next to Page
        /// </summary>
        private string _NEXT_TO_PAGE = "Next to Page";
        [Category("Globalization")]
        public string NextToPageClause
        {
            get { return _NEXT_TO_PAGE; }
            set { _NEXT_TO_PAGE = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "Last Page" in the pager control.
        /// Default value: &gt;&gt;
        /// </summary>
        private string _LAST = "&gt;&gt;";
        [Category("Globalization")]
        public string LastClause
        {
            get { return _LAST; }
            set { _LAST = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "Last Page" in the pager control.
        /// Default value: &gt;&gt;
        /// </summary>
        private string _LASTIMAGEURL = string.Empty;
        [Category("Globalization")]
        public string LastClauseImageUrl
        {
            get { return _LASTIMAGEURL; }
            set { _LASTIMAGEURL = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "Last Page" in the pager control.
        /// Default value: &gt;&gt;
        /// </summary>
        private string _LASTIMAGEDISABLEDURL = string.Empty;
        [Category("Globalization")]
        public string LastClauseImageDisabledUrl
        {
            get { return _LASTIMAGEDISABLEDURL; }
            set { _LASTIMAGEDISABLEDURL = value; }
        }


        /// <summary>
        /// Gets or sets the text caption displayed as "First Page" in the pager control.
        /// Default value: &lt;&lt;
        /// </summary>
        private string _FIRST = "&lt;&lt;";
        [Category("Globalization")]
        public string FirstClause
        {
            get { return _FIRST; }
            set { _FIRST = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "First Page" in the pager control.
        /// Default value: &lt;&lt;
        /// </summary>
        private string _FIRSTIMAGEURL = string.Empty;
        [Category("Globalization")]
        public string FirstClauseImageUrl
        {
            get { return _FIRSTIMAGEURL; }
            set { _FIRSTIMAGEURL = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "First Page" in the pager control.
        /// Default value: &lt;&lt;
        /// </summary>
        private string _FIRSTIMAGEDISABLEDURL = string.Empty;
        [Category("Globalization")]
        public string FirstClauseImageDisabledUrl
        {
            get { return _FIRSTIMAGEDISABLEDURL; }
            set { _FIRSTIMAGEDISABLEDURL = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "Previous Page" in the pager control.
        /// Default value: &lt;
        /// </summary>
        private string _previous = "&lt;";
        [Category("Globalization")]
        public string PreviousClause
        {
            get { return _previous; }
            set { _previous = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "Previous Page" in the pager control.
        /// Default value: &lt;
        /// </summary>
        private string _previousImageUrl = string.Empty;
        [Category("Globalization")]
        public string PreviousClauseImageUrl
        {
            get { return _previousImageUrl; }
            set { _previousImageUrl = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "Previous Page" in the pager control.
        /// Default value: &lt;
        /// </summary>
        private string _previousImageDisabledUrl = string.Empty;
        [Category("Globalization")]
        public string PreviousClauseImageDisabledUrl
        {
            get { return _previousImageDisabledUrl; }
            set { _previousImageDisabledUrl = value; }
        }
        /// <summary>
        /// Gets or sets the text caption displayed as "Next Page" in the pager control.
        /// Default value: &gt;
        /// </summary>
        private string _next = "&gt;";
        [Category("Globalization")]
        public string NextClause
        {
            get { return _next; }
            set { _next = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "Next Page" in the pager control.
        /// Default value: &gt;
        /// </summary>
        private string _nextImageUrl = string.Empty;
        [Category("Globalization")]
        public string NextClauseImageUrl
        {
            get { return _nextImageUrl; }
            set { _nextImageUrl = value; }
        }

        /// <summary>
        /// Gets or sets the text caption displayed as "Next Page" in the pager control.
        /// Default value: &gt;
        /// </summary>
        private string _nextImageDisabledUrl = string.Empty;
        [Category("Globalization")]
        public string NextClauseImageDisabledUrl
        {
            get { return _nextImageDisabledUrl; }
            set { _nextImageDisabledUrl = value; }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether pager control should render RTL or LTR.
        /// </summary>
        private bool _rightToLeft = false;
        [Category("Globalization")]
        public bool RTL
        {
            get { return _rightToLeft; }
            set { _rightToLeft = value; }
        }


        #endregion

        #region // Render Utilities
        private string GenerateAltMessage(int pageNumber)
        {
            StringBuilder altGen = new StringBuilder();
            altGen.Append(pageNumber == CurrentIndex ? ShowingResultClause : ShowResultClause);
            altGen.Append(" ");
            altGen.Append(((pageNumber - 1) * PageSize) + 1);
            altGen.Append(" ");
            altGen.Append(ToClause);
            altGen.Append(" ");
            altGen.Append(pageNumber == PageCount ? VirtualItemCount : pageNumber * PageSize);
            altGen.Append(" ");
            altGen.Append(OfClause);
            altGen.Append(" ");
            altGen.Append(VirtualItemCount);

            return altGen.ToString();
        }

        private string GetAlternativeText(int pageNumber)
        {
            return GenerateToolTips ? string.Format(" title=\"{0}\"", GenerateAltMessage(pageNumber)) : "";
        }

        private string RenderFirst(bool enabled)
        {
			string templateCell = "<td class=\"PagerOtherPageCells\"><a name=\"" + this.UniqueID + "\" id=\"" + this.ClientID + "\" class=\"PagerHyperlinkStyle\" href=\"{0}\" title=\"" + " " + BackToFirstClause + " " + "\"> " + (string.IsNullOrEmpty(FirstClauseImageUrl) ? FirstClause : FirstClauseImageUrl) + " </a></td><td>...&nbsp;</td>";
            if (!enabled)
            {
                //templateCell = "<td class=\"PagerOtherPageCells\"> " + (string.IsNullOrEmpty(FirstClauseImageDisabledUrl) ? FirstClause : FirstClauseImageDisabledUrl) + " </td>";
				templateCell = string.Empty;
            }
            return String.Format(templateCell, Page.ClientScript.GetPostBackClientHyperlink(this, "1"));
        }

        private string RenderLast(bool enabled)
        {
			string templateCell = "<td>...&nbsp;</td><td class=\"PagerOtherPageCells\"><a name=\"" + this.UniqueID + "\" id=\"" + this.ClientID + "\" class=\"PagerHyperlinkStyle\" href=\"{0}\" title=\"" + " " + GoToLastClause + " " + "\"> " + (string.IsNullOrEmpty(LastClauseImageUrl) ? LastClause : LastClauseImageUrl) + " </a></td>";
            if (!enabled)
            {
				templateCell = "<td class=\"PagerOtherPageCells\"> " + (string.IsNullOrEmpty(LastClauseImageDisabledUrl) ? LastClause : LastClauseImageDisabledUrl) + " </td>";
            }
            return String.Format(templateCell, Page.ClientScript.GetPostBackClientHyperlink(this, PageCount.ToString()));
        }

        private string RenderBack(bool enabled)
        {
            string templateCell = "<td class=\"PagerOtherPageCells\"><a name=\"" + this.UniqueID + "\" id=\"" + this.ClientID + "\" class=\"PagerHyperlinkStyle\" href=\"{0}\" title=\"" + " " + BackToPageClause + " " + (CurrentIndex - 1).ToString() + "\"> " + (string.IsNullOrEmpty(PreviousClauseImageUrl) ? PreviousClause : PreviousClauseImageUrl) + " </a></td>";
            if (!enabled)
            {
                templateCell = "<td class=\"PagerOtherPageCells\"> " + (string.IsNullOrEmpty(PreviousClauseImageDisabledUrl) ? PreviousClause : PreviousClauseImageDisabledUrl) + " </td>";
            }
            return String.Format(templateCell, Page.ClientScript.GetPostBackClientHyperlink(this, (CurrentIndex - 1).ToString()));
        }

        private string RenderNext(bool enabled)
        {
            string templateCell = "<td class=\"PagerOtherPageCells\"><a name=\"" + this.UniqueID + "\" id=\"" + this.ClientID + "\" class=\"PagerHyperlinkStyle\" href=\"{0}\" title=\"" + " " + NextToPageClause + " " + (CurrentIndex + 1).ToString() + "\"> " + (string.IsNullOrEmpty(NextClauseImageUrl) ? NextClause : NextClauseImageUrl) + " </a></td>";
            if (!enabled)
            {
                templateCell = "<td class=\"PagerOtherPageCells\"> " + (string.IsNullOrEmpty(NextClauseImageDisabledUrl) ? NextClause : NextClauseImageDisabledUrl) + " </td>";
            }
            return String.Format(templateCell, Page.ClientScript.GetPostBackClientHyperlink(this, (CurrentIndex + 1).ToString()));
        }

        private string RenderCurrent()
        {
			if (CurrentIndex < 1000)
				return "<td class=\"PagerCurrentPageCell\"><span class=\"PagerHyperlinkStyle\" " + GetAlternativeText(CurrentIndex) + " ><strong> " + CurrentIndex.ToString() + " </strong></span></td>";
			else
				return "<td class=\"PagerCurrentPageCell\"><span class=\"PagerHyperlinkStyle\" " + GetAlternativeText(CurrentIndex) + " ><strong> " + string.Format("{0:0,00}",CurrentIndex) + " </strong></span></td>";
        }

        private string RenderOther(int pageNumber)
        {
			string templateCell = string.Empty;
			if (pageNumber < 1000)
				templateCell = "<td class=\"PagerOtherPageCells\"><a class=\"PagerHyperlinkStyle\" href=\"{0}\" " + GetAlternativeText(pageNumber) + " > " + pageNumber.ToString() + " </a></td>";
			else
				templateCell = "<td class=\"PagerOtherPageCells\"><a class=\"PagerHyperlinkStyle\" href=\"{0}\" " + GetAlternativeText(pageNumber) + " > " + string.Format("{0:0,00}", pageNumber) + " </a></td>";
            return String.Format(templateCell, Page.ClientScript.GetPostBackClientHyperlink(this, pageNumber.ToString()));
        }

        private string RenderOtherMore(int pageNumber)
        {
            string templateCell = "<td class=\"PagerOtherPageCells\"><a class=\"PagerHyperlinkStyle\" href=\"{0}\" " + GetAlternativeText(pageNumber) + " > ... </a></td>";
            return String.Format(templateCell, Page.ClientScript.GetPostBackClientHyperlink(this, pageNumber.ToString()));
        }

        private string RenderSSC(int pageNumber)
        {
            string templateCell = "<td class=\"PagerSSCCells\"><a class=\"PagerHyperlinkStyle\" href=\"{0}\" " + GetAlternativeText(pageNumber) + " > " + pageNumber.ToString() + " </a></td>";
            return String.Format(templateCell, Page.ClientScript.GetPostBackClientHyperlink(this, pageNumber.ToString()));
        }

        private string RenderGoTo()
        {
            string templateCell = "<td style=\"padding:1px 1px 1px 1px;\" class=\"PagerOtherPageCells\"><div onclick=\"handleGoToVisibility()\" class=\"GoToLabel\">&nbsp;Go To&nbsp;</div><img id=\"goto_img\" onclick=\"handleGoToVisibility()\" src=\"" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "ASPnetPagerV2_8.Images.arr_right.gif") + "\" alt=\"\" class=\"GoToArrow\"/>&nbsp;<div id=\"div_goto\" style=\"display:none;\"><select class=\"GoToSelect\" name=\"ddlTes\" id=\"ddlTes\" onchange=\"javascript:handleGoto(this);\">{0}</select></div></td>";
            string listItemTemplate = "<option {0} value=\"{1}\">{2}</option>";

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= this.PageCount; i++)
            {
                sb.Append(string.Format(listItemTemplate, i == CurrentIndex ? "selected=\"selected\" class=\"GoToSelectedOption\"" : "", Page.ClientScript.GetPostBackClientHyperlink(this, i.ToString()), i));
            }
            return string.Format(templateCell, sb.ToString());
        }

        // LLD: disable this feature
        private string RenderGoToScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"
                                function handleGoto(selectObj) {
                                    eval(selectObj.options[selectObj.selectedIndex].value);
                                }

                                function handleGoToVisibility() {
                                    var gotoElem = document.getElementById('div_goto');
                                    gotoElem.style.display = gotoElem.style.display == 'none' ? 'inline' : 'none';
                                    var gotoImg = document.getElementById('goto_img');
                                    
                                ");

            sb.AppendFormat("gotoImg.src = gotoElem.style.display == 'none' ? '{0}' : '{1}';",
                                    Page.ClientScript.GetWebResourceUrl(this.GetType(), "ASPnetPagerV2_8.Images.arr_right.gif"),
                                    Page.ClientScript.GetWebResourceUrl(this.GetType(), "ASPnetPagerV2_8.Images.arr_left.gif")
                                    );
            sb.Append("}");



            string goToScript = "<script type=\"text/javascript\">{0}</script>";

            return string.Format(goToScript, sb.ToString());
        }
        #endregion

        #region // Smart ShortCut Stuff

        private List<int> _smartShortCutList;
        private List<int> SmartShortCutList
        {
            get { return _smartShortCutList; }
            set { _smartShortCutList = value; }
        }

        private void CalculateSmartShortcutAndFillList()
        {
            _smartShortCutList = new List<int>();
            double shortCutCount = this.PageCount * SmartShortCutRatio / 100;
            double shortCutCountRounded = System.Math.Round(shortCutCount, 0);
            if (shortCutCountRounded > MaxSmartShortCutCount) shortCutCountRounded = MaxSmartShortCutCount;
            if (shortCutCountRounded == 1) shortCutCountRounded++;

            for (int i = 1; i < shortCutCountRounded + 1; i++)
            {
                int calculatedValue = (int)(System.Math.Round((this.PageCount * (100 / shortCutCountRounded) * i / 100) * 0.1, 0) * 10);
                if (calculatedValue >= this.PageCount) break;
                SmartShortCutList.Add(calculatedValue);
            }
        }

        /* smart shortcut list calculator and list */
        private void RenderSmartShortCutByCriteria(int basePageNumber, bool getRightBand, HtmlTextWriter writer)
        {
            if (IsSmartShortCutAvailable())
            {

                List<int> lstSSC = this.SmartShortCutList;

                int rVal = -1;
                if (getRightBand)
                {
                    for (int i = 0; i < lstSSC.Count; i++)
                    {
                        if (lstSSC[i] > basePageNumber)
                        {
                            rVal = i;
                            break;
                        }
                    }
                    if (rVal >= 0)
                    {
                        for (int i = rVal; i < lstSSC.Count; i++)
                        {
                            if (lstSSC[i] != basePageNumber)
                            {
                                writer.Write(RenderSSC(lstSSC[i]));
                            }
                        }
                    }
                }
                else if (!getRightBand)
                {

                    for (int i = 0; i < lstSSC.Count; i++)
                    {
                        if (basePageNumber > lstSSC[i])
                        {
                            rVal = i;
                        }
                    }

                    if (rVal >= 0)
                    {
                        for (int i = 0; i < rVal + 1; i++)
                        {
                            if (lstSSC[i] != basePageNumber)
                            {
                                writer.Write(RenderSSC(lstSSC[i]));
                            }
                        }
                    }
                }
            }
        }

        bool IsSmartShortCutAvailable()
        {
            return this.GenerateSmartShortCuts && this.SmartShortCutList != null && this.SmartShortCutList.Count != 0;
        }
        #endregion

        #region // Render "SearchEngineFriendly" hyperlinks in HiddenDiv
        private string RenderHiddenDiv()
        {
            System.Text.RegularExpressions.Regex regEx;
            Uri theURL = HttpContext.Current.Request.Url;
            bool hasQueryStringParam = !string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["QUERY_STRING"]) ? true : false;
            string tempHyperlink = "<a href=\"{0}\">page {1}</a>";
            string tempDiv = "<div style=\"display:none;\">{0}</div>";
            StringBuilder sb = new StringBuilder();

            if (hasQueryStringParam && System.Web.HttpContext.Current.Request.QueryString[this.QueryStringParameterName] != null)
            {
                regEx = new Regex(this.QueryStringParameterName + @"\=\d*", RegexOptions.Compiled | RegexOptions.Singleline);
                for (int i = 0; i < this.NormalModePageCount; i++)
                {
                    sb.Append(string.Format(tempHyperlink,
                                regEx.Replace(theURL.ToString(), this.QueryStringParameterName + "=" + (i + this.CurrentIndex)), i + this.CurrentIndex)
                        );
                }
            }
            else
            {
                string qsParameterName = "";
				string absoluteUrl = GetRequestAbsoluteUriByUri();

                for (int i = 0; i < this.NormalModePageCount; i++)
                {
                    qsParameterName = string.Format("{0}={1}", this.QueryStringParameterName, i + this.CurrentIndex);
                    sb.Append(string.Format(tempHyperlink,
								hasQueryStringParam ? absoluteUrl + "&" + qsParameterName : absoluteUrl + "?" + qsParameterName,
                                i + this.CurrentIndex)
                            );
                }

            }

            return string.Format(tempDiv, sb.ToString());
        }

		/// <summary>
		///		Get the absolution url by Uri
		/// </summary>
		/// <returns>
		///		Return the absolution url by Uri
		/// </returns>
		/// <remarks>
		///		We assume that our web sites are using port 80.
		/// </remarks>
		private string GetRequestAbsoluteUriByUri()
		{
			string absoluteUrl = HttpContext.Current.Request.Url.AbsoluteUri;

			if (!string.IsNullOrEmpty(absoluteUrl))
			{
				string pattern = @"(https?://[^/]+):\d+(\S*)";

				Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);

				if (reg.IsMatch(absoluteUrl))
				{
					absoluteUrl = reg.Replace(absoluteUrl, "$1$2");
				}
			}

			return absoluteUrl;
		}
		#endregion

        #region // Override Control's Render operation
        protected override void Render(HtmlTextWriter writer)
        {

            if (Page != null) Page.VerifyRenderingInServerForm(this);

			//if (this.VirtualItemCount == 0)
			//{
			//    this.Visible = false;
			//    return;
			//}

			if (!(this.PageCount > 1))
			{
				this.Visible = false;
				return;
			}

			this.FirstClause = "1";
			this.LastClause = (this.PageCount < 1000) ? this.PageCount.ToString() : string.Format("{0:0,00}", this.PageCount);
			
			if (this.PageCount > this.SmartShortCutThreshold && GenerateSmartShortCuts)
            {
                CalculateSmartShortcutAndFillList();
            }


			if (GeneratePagerInfoSection)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "PagerInfoCell");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.Write(_virtualItemCount.ToString("#,##") + " " + PageClause);
                writer.RenderEndTag();
            }

			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "3");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "1");
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "PagerContainerTable");
			if (RTL) writer.AddAttribute(HtmlTextWriterAttribute.Dir, "rtl");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            //if (GenerateFirstLastSection && CurrentIndex != 1)
			if (GenerateFirstLastSection)
			{
				if (CurrentIndex >= CompactModePageCount)
					writer.Write(RenderFirst(CurrentIndex != 1));
			}

            //if (CurrentIndex != 1)
            //writer.Write(RenderBack(CurrentIndex != 1));
            if (CurrentIndex < CompactModePageCount)
            {

                if (CompactModePageCount > PageCount) CompactModePageCount = PageCount;

                for (int i = 1; i < CompactModePageCount + 1; i++)
                {
                    if (i == CurrentIndex)
                    {
                        writer.Write(RenderCurrent());
                    }
                    else
                    {
                        writer.Write(RenderOther(i));
                    }
                }
                //if (!GenerateSmartShortCuts)
                //    writer.Write(RenderOtherMore(CompactModePageCount + 1));
                //else
                    RenderSmartShortCutByCriteria(CompactModePageCount, true, writer);

            }
            else if (CurrentIndex >= CompactModePageCount && CurrentIndex < NormalModePageCount)
            {

                if (NormalModePageCount > PageCount) NormalModePageCount = PageCount;

                for (int i = 1; i < NormalModePageCount + 1; i++)
                {
                    if (i == CurrentIndex)
                    {
                        writer.Write(RenderCurrent());
                    }
                    else
                    {
                        writer.Write(RenderOther(i));
                    }
                }
                //if (!GenerateSmartShortCuts)
                //    writer.Write(RenderOtherMore(NormalModePageCount + 1));
                //else
                    RenderSmartShortCutByCriteria(NormalModePageCount, true, writer);

            }
            else if (CurrentIndex >= NormalModePageCount)
            {
                int gapValue = NormalModePageCount / 2;
                int leftBand = CurrentIndex - gapValue;
                int rightBand = CurrentIndex + gapValue;

				//etc:1,2,3,4,5,6,7,8,9  currentIndex is 6:must be show 5,6,7,8,9 so add one
				if (CurrentIndex == PageCount - NormalModePageCount + gapValue)
				{
					leftBand = leftBand + 1;
					rightBand = rightBand + 1;
				}

                //if (!GenerateSmartShortCuts)
                //    writer.Write(RenderOtherMore(leftBand - 1));
                //else
                    RenderSmartShortCutByCriteria(leftBand, false, writer);

                for (int i = leftBand; (i < rightBand + 1) && i < PageCount + 1; i++)
                {
                    if (i == CurrentIndex)
                    {
                        writer.Write(RenderCurrent());
                    }
                    else
                    {
                        writer.Write(RenderOther(i));
                    }
                }

                if (rightBand < this.PageCount)
                {
                    //if (!GenerateSmartShortCuts)
                    //    writer.Write(RenderOtherMore(rightBand + 1));
                    //else
                        RenderSmartShortCutByCriteria(rightBand, true, writer);
                }
            }

            //if (CurrentIndex != PageCount)
            //writer.Write(RenderNext(CurrentIndex != PageCount));

            //if (GenerateFirstLastSection && CurrentIndex != PageCount)
			if (GenerateFirstLastSection)
			{
				//if (PageCount >= CompactModePageCount && ((CurrentIndex + (CompactModePageCount-1)/2 +1)<= PageCount))
				if (PageCount > NormalModePageCount && (CurrentIndex + NormalModePageCount - 1 <= PageCount || CurrentIndex == (PageCount + 1) / 2))
					writer.Write(RenderLast(CurrentIndex != PageCount));
			}

            // LLD: disable this feature
            //if (GenerateGoToSection)
            //    writer.Write(RenderGoTo());

            writer.RenderEndTag();

            writer.RenderEndTag();

            // LLD: disable this feature
            //if (GenerateGoToSection)
            //    writer.Write(RenderGoToScript());

            if (GenerateHiddenHyperlinks)
                writer.Write(RenderHiddenDiv());
        }
        #endregion
    }
}
