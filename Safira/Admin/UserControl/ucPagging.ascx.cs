using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ucPagging : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int MaxPage { get; set; }
    public string URL { get; set; }
    public void DataBind()
    {
        var pager = new Pager(TotalItems, CurrentPage, PageSize, MaxPage);

        string pageFirstUrl = string.Format(URL, pager.PageFirst);
        string pageLastUrl = string.Format(URL, pager.PageLast);
        string pageBackUrl = string.Format(URL, pager.PagePrev);
        string pageNextUrl = string.Format(URL, pager.PageNext);
      
        PageFirst.HRef = pageFirstUrl;
        PageLast.HRef = pageLastUrl;
        PageBack.HRef = pageBackUrl;
        PageNext.HRef = pageNextUrl;

        CurrentPageValue.InnerHtml = CurrentPage.ToString();
        TotalPagesValue.InnerHtml = pager.TotalPages.ToString();
        TotalItemsValue.InnerHtml = pager.TotalItems.ToString();



        Dictionary<int, string> pageNumbers = new Dictionary<int, string>();

        for (int i = pager.StartPage; i <= pager.EndPage; i++)
        {
            pageNumbers.Add(i, string.Format(URL, i));
        }

        PageRepeater.DataSource = pageNumbers;
        PageRepeater.DataBind();

    }
}