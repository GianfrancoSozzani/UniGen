using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CaricaChart();
            CaricaFacolta();
            CaricaAnno();
        }

    }

    public void CaricaChart()
    {
        PAGAMENTI paga = new PAGAMENTI();
        DataTable miaDataTable = paga.IncassiPerAnno();
        Chart1.Series["Series1"].Points.Clear();

        foreach (DataRow row in miaDataTable.Rows)
        {
            string corso = row["Anno"].ToString();
            decimal importo = Convert.ToDecimal(row["Importo"]);

            Chart1.Series["Series1"].Points.AddXY(corso, importo);
        }
        Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Anno";
        Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Importo Pagato (€)";
        Chart1.Series["Series1"].IsValueShownAsLabel = true;

        Chart1.Series["Series1"].Color = System.Drawing.Color.SteelBlue;
        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
    }

    public void CaricaFacolta()
    {
        FACOLTA fac = new FACOLTA();
        ddlFacolta.DataSource = fac.SelezionaTutto();
        ddlFacolta.DataTextField = "TitoloFacolta";
        ddlFacolta.DataValueField = "K_Facolta";
        ddlFacolta.DataBind();
    }

    public void CaricaAnno()
    {
        PAGAMENTI pag = new PAGAMENTI();
        ddlAnno.DataSource = pag.IncassiPerAnno();
        ddlAnno.DataTextField = "Anno";
        ddlAnno.DataValueField = "Anno";
        ddlAnno.DataBind();
    }
}