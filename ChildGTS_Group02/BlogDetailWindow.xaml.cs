using ChildGTS_Group02.DAL.Entities;
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
using System.Windows.Shapes;

namespace ChildGTS_Group02
{
    /// <summary>
    /// Interaction logic for BlogDetailWindow.xaml
    /// </summary>
    public partial class BlogDetailWindow : Window
    {
        public BlogDetailWindow(Blog blog)
        {
            InitializeComponent();
            TitleTextBlock.Text = blog.Title;
            CategoryTextBlock.Text = blog.Category;
            AuthorTextBlock.Text = blog.Author?.FullName ?? "Unknown";
            ContentTextBlock.Text = blog.Content;
        }
    }
}
