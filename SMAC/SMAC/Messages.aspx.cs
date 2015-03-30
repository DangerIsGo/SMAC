using SMAC.Database;
using System;
using System.Collections.Generic;
using System.Data;

namespace SMAC
{
    public partial class Messages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Request.Cookies["SmacCookie"]["UserId"];

            if (Request.QueryString["msgId"] == null)
            {
                var msgs = PrivateMessageEntity.GetLatestPrivateMessages(userId, 1, 20);

                var list = new List<string> { "Id", "Name", "Date", "LastMessage", "UnRead" };

                DataTable table = new DataTable();

                foreach (var item in list)
                    table.Columns.Add(item, typeof(string));

                //Now add some rows(which will be repeated in the ItemTemplate)
                foreach (var msg in msgs)
                {
                    table.Rows.Add(
                        msg.PrivateMessageId,
                        msg.ToUser == userId ? msg.SenderFN + " " + msg.SenderLN : msg.ReceiverFN + " " + msg.ReceiverLN,
                        msg.DateSent,
                        msg.Content,
                        msg.DateRead == null && msg.ToUser == userId ? "true" : "false");
                }

                messageListView.DataSource = table;
                messageListView.DataBind();
            }
            else
            {
                var msg = PrivateMessageEntity.GetPrivateMessage(int.Parse(Request.QueryString["msgId"]));
                var msgs = PrivateMessageEntity.GetPrivateMessageThread(msg.ToUser, msg.FromUser);


            }
        }
    }
}