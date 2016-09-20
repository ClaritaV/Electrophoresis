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
			var bitmap = new System.Drawing.Bitmap(textBoxImagePath.Text);
			var canvas = System.Drawing.Graphics.FromImage(bitmap);
			foreach (var seed in result.Seeds)
			{
				canvas.DrawRectangle(System.Drawing.Pens.Red, seed.Left, 0, seed.Right-seed.Left, bitmap.Height);
				foreach (var protein in seed.Proteins)
				{
					canvas.DrawRectangle(System.Drawing.Pens.LimeGreen, seed.Left + Seed.InflateDelta, protein.Top, seed.Right - seed.Left - Seed.InflateDelta * 2, protein.Bottom - protein.Top);
				}
			}
			imagePreview.Source = bitmap.ToSource();
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
