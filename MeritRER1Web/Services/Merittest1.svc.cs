using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.EventReceivers;

namespace MeritRER1Web.Services
{
    public class Merittest1 : IRemoteEventService
    {
        /// <summary>
        /// Handles events that occur before an action occurs, such as when a user adds or deletes a list item.
        /// </summary>
        /// <param name="properties">Holds information about the remote event.</param>
        /// <returns>Holds information returned from the remote event.</returns>
        public SPRemoteEventResult ProcessEvent(SPRemoteEventProperties properties)
        {
            SPRemoteEventResult result = new SPRemoteEventResult();

            using (ClientContext clientContext = TokenHelper.CreateRemoteEventReceiverClientContext(properties))
            {
                if (clientContext != null)
                {
                    clientContext.Load(clientContext.Web);
                    clientContext.ExecuteQuery();
                    //if (
                    //  properties.ItemEventProperties.ListTitle.Equals("Merittest1", StringComparison.OrdinalIgnoreCase)){
                    // List merit1 = clientContext.Web.Lists.GetByTitle("Merittest1");
                    // ListItem item = merit1.GetItemById(
                    //  properties.ItemEventProperties.ListItemId);
                    // clientContext.Load(item);
                    // clientContext.ExecuteQuery();

                    // item["Title"] += "\nUpdated by RER " +
                    //    System.DateTime.Now.ToLongTimeString();
                    // item.Update();
                    // clientContext.ExecuteQuery();
                    //}
                }
            }

            return result;
        }

        /// <summary>
        /// Handles events that occur after an action occurs, such as after a user adds an item to a list or deletes an item from a list.
        /// </summary>
        /// <param name="properties">Holds information about the remote event.</param>
        public void ProcessOneWayEvent(SPRemoteEventProperties properties)
        {
            using (ClientContext clientContext = TokenHelper.CreateRemoteEventReceiverClientContext(properties))
            {
                if (clientContext != null)
                {
                    clientContext.Load(clientContext.Web);
                    clientContext.ExecuteQuery();

                    SPRemoteEventResult result = new SPRemoteEventResult();

                    Uri myurl = new Uri(properties.ItemEventProperties.WebUrl);


                    if (clientContext != null)
                    {
                        if (properties.EventType == SPRemoteEventType.ItemAdded)
                        {
                            List lstLog = clientContext.Web.Lists.GetByTitle("EventTrackLog");
                            clientContext.Load(lstLog);
                            clientContext.ExecuteQuery();


                            if (
                      properties.ItemEventProperties.ListTitle.Equals("Merittest1", StringComparison.OrdinalIgnoreCase))
                            {
                                List merit1 = clientContext.Web.Lists.GetByTitle("Merittest1");
                                ListItem item = merit1.GetItemById(
                                 properties.ItemEventProperties.ListItemId);
                                clientContext.Load(item);
                                clientContext.ExecuteQuery();

                                item["Title"] += "\nUpdated by RER " +
                                   System.DateTime.Now.ToLongTimeString();
                                item.Update();
                                clientContext.ExecuteQuery();
                            }


                        }
                    }
                }
            }
        }
    }

}