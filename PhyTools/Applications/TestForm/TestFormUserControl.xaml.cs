using PhyTools.Delegates;
using PhyTools.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhyTools.Applications.TestForm
{
    /// <summary>
    /// Interaction logic for TestFormUserControl.xaml
    /// </summary>
    public partial class TestFormUserControl : UserControl
    {
        public event PrintToConsole PrintToConsoleEvent;
        public event NotificationDelegate NotificationCountEvent;

        public TestFormUserControl()
        {
            InitializeComponent();
            AddItemsToList();
        }

        private void Button_Click_SendLog(object sender, RoutedEventArgs e)
        {
            if (PrintToConsoleEvent != null)
            {
                PrintToConsoleEvent.Invoke(null, new PrintToConsoleEvent()
                {
                    Text = TextBoxPrintToConsole.Text,
                });
            }
        }

        private void Button_Click_IncrementNotificationCount(object sender, RoutedEventArgs e)
        {
            if (NotificationCountEvent != null)
            {
                NotificationCountEvent.Invoke(null, new NotificationCountEvent()
                {
                    Amount = 1,
                });
            }
        }

        private void Button_Click_DecrementNotificationCount(object sender, RoutedEventArgs e)
        {
            if (NotificationCountEvent != null)
            {
                NotificationCountEvent.Invoke(null, new NotificationCountEvent()
                {
                    Amount = -1,
                });
            }
        }

        #region drag and drop
        private Point StartPoint { get; set; }

        private void AddItemsToList()
        {
            for (int x = 0; x < 2; x++)
            {
                DragList.Items.Add(new Contact() { Name = x.ToString() });
            }
        }

        #region drag
        private void List_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Store the mouse position
            StartPoint = e.GetPosition(null);
        }

        private void List_MouseMove(object sender, MouseEventArgs e)
        {
            if (StartPoint == null)
            {
                return;
            }

            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = StartPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed && Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance && Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                // Get the dragged ListViewItem 
                ListView listView = sender as ListView;

                var items = listView.SelectedItems.Cast<Contact>();

                var listOfItems = items.ToList();

                if (listOfItems.Count == 0)
                    return;


                // Initialize the drag & drop operation 
                DataObject dragData = new DataObject("myFormat", listOfItems);
                DragDrop.DoDragDrop(listView, dragData, DragDropEffects.Move);
            }


            /*
            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listViewItem != null)
                {
                    // Find the data behind the ListViewItem
                    Contact contact = (Contact)listView.ItemContainerGenerator.
                        ItemFromContainer(listViewItem);

                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("myFormat", contact);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
            }
            */
        }
        #endregion

        #region drop
        private void DropList_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DropList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                List<Contact> contacts = e.Data.GetData("myFormat") as List<Contact>;

                if (contacts == null)
                    return;

                foreach (var contact in contacts)
                {
                    ListView listView = sender as ListView;
                    listView.Items.Add(contact);
                }
            }
        }
        #endregion

        #endregion

        // Helper to search up the VisualTree
        private T FindAnchestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
        
    }

    public class Contact
    {
        public string Name { get; set; }
    }
}