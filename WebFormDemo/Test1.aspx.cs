using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormDemo
{
    public partial class Test1 : System.Web.UI.Page
    {
        public class Like
        {
            public int ID { get; set; }
            public string name { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            List<Like> likes = new List<Like>() {
            new Like(){
            ID=1,name="足球"}, new Like()
            {
                ID = 2,
                name = "蓝球"
            }, new Like()
            {
                ID = 2,
                name = "羽毛球"
            }, };
            chkLike.Items.Clear();
            foreach (var like in likes)
            {
                ListItem item = new ListItem();
                item.Text = like.name;
                item.Value = like.ID.ToString();
                chkLike.Items.Add(item);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var name = txtName.Text;
            var sex = Sex.SelectedValue;
            foreach (ListItem item in chkLike.Items)
            {
                if (item.Selected)
                {
                    var val = item.Value;
                }
            }
            var edo = rdoEducation.SelectedValue;
            var IsDy = chkIsDy.Checked;
            Response.Redirect("Test2.aspx?id=1");
        }
    }
}