using System.Windows;
using System.Windows.Controls;

namespace dDeltaSolutions.Spy
{
    public partial class MainWindow
    {
        private void OnActions(object sender, RoutedEventArgs e)
        {
            TreeViewItem treeviewItem = tvElements.SelectedItem as TreeViewItem;
            if (treeviewItem == null)
            {
                MessageBox.Show("Select an element in the tree");
                return;
            }

            TreeNode node = treeviewItem.Tag as TreeNode;
            if (node == null)
            {
                return;
            }

            if (node.IsAlive == false)
            {
                MessageBox.Show("The selected element is not available anymore");
                return;
            }

            bool timerStopped = false;
            if (timer != null && timer.Enabled)
            {
                timer.Enabled = false;
                timerStopped = true;
            }

            bool timerTrackStopped = false;
            if (timerTrack != null && timerTrack.Enabled)
            {
                timerTrack.Enabled = false;
                timerTrackStopped = true;
            }

            WindowActions wndActions = new WindowActions(node)
            {
                Owner = this
            };
            wndActions.ShowDialog();

            if (timerStopped)
            {
                timer.Enabled = true;
            }

            if (timerTrackStopped)
            {
                timerTrack.Enabled = true;
            }
        }
    }
}