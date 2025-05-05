using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaClassi;

public partial class _Default : System.Web.UI.Page
{
    string annoscelto = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CaricaChart();
            CaricaAnno();
            CaricaRp();
        }

    }

    public void CaricaChart()
    {
        PAGAMENTI paga = new PAGAMENTI();
        paga.Anno = annoscelto;
        if (paga.Anno == "0")
        {
            DataTable dT = paga.IncassiGroupByAnno();
            Chart1.Series["Series1"].Points.Clear();

            foreach (DataRow row in dT.Rows)
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
        else
        {
            DataTable dT = paga.IncassiPerAnno();
            Chart1.Series["Series1"].Points.Clear();

            foreach (DataRow row in dT.Rows)
            {
                string corso = row["Corso"].ToString();
                decimal importo = Convert.ToDecimal(row["Importo"]);

                Chart1.Series["Series1"].Points.AddXY(corso, importo);
            }
            Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Corso";
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Importo Pagato (€)";
            Chart1.Series["Series1"].IsValueShownAsLabel = true;

            Chart1.Series["Series1"].Color = System.Drawing.Color.SteelBlue;
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
        }
        DataBind();
    }
    public void CaricaAnno()
    {
        PAGAMENTI pag = new PAGAMENTI();
        ddlAnno.DataSource = pag.IncassiGroupByAnno();
        ddlAnno.DataTextField = "Anno";
        ddlAnno.DataValueField = "Anno";
        ddlAnno.Items.Insert(0, new ListItem("Seleziona Anno", "0"));
        ddlAnno.DataBind();
    }

    protected void ddlAnno_SelectedIndexChanged(object sender, EventArgs e)
    {
        annoscelto = ddlAnno.SelectedValue;
        CaricaChart();
        CaricaRp();
    }

    protected void btnRiepilogo_Click(object sender, EventArgs e)
    {
        annoscelto = "0";
        CaricaChart();
        CaricaRp();
    }
    public void CaricaRp()
    {
        PAGAMENTI pag = new PAGAMENTI();
        pag.Anno = annoscelto;
        DataTable dt = new DataTable();
        if (pag.Anno == "0")
        {
            pag.Anno = DateTime.Now.Year.ToString();
            dt = pag.IncassiGroupByFacolta();
        }
        else
        {
            dt = pag.IncassiGroupByFacolta();
        }
        decimal totaleImporto = 0;
        int totaleIscritti = 0;

        foreach (DataRow row in dt.Rows)
        {
            totaleImporto += row.Field<decimal>("Importo");
            totaleIscritti += row.Field<int>("Iscritti");
        }

        // Aggiungi riga totale
        DataRow totaleRow = dt.NewRow();
        totaleRow["Importo"] = totaleImporto;
        totaleRow["Iscritti"] = totaleIscritti;
        totaleRow["Facolta"] = "TOTALE";

        dt.Rows.InsertAt(totaleRow, 0); // Inserisce in cima

        rptIncassiFacolta.DataSource = dt;
        rptIncassiFacolta.DataBind();
    }
}