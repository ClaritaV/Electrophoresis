using System;
using System.Windows;
using System.Windows.Media.Imaging;

using Microsoft.Win32;

namespace Electrophoresis
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private readonly OpenFileDialog openImageDialog = new OpenFileDialog
		{
			DefaultExt = ".jpg",
			Filter = "JPEG-изображение|*.jpg",
			RestoreDirectory = true,
			Title = "Выберите изображение для анализа",
		};

		private void analysisClick(object sender, RoutedEventArgs e)
		{
			var result = new Electropherogram(textBoxImagePath.Text);
		}

		private void browseFileClick(object sender, RoutedEventArgs e)
		{
			if (openImageDialog.ShowDialog() == true)
			{
				textBoxImagePath.Text = openImageDialog.FileName;
			}
		}

		private void showImageClick(object sender, RoutedEventArgs e)
		{
			try
			{
				imagePreview.Source = new BitmapImage(new Uri(textBoxImagePath.Text));
			}
			catch
			{
				MessageBox.Show("Невозможно загрузить изображение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
