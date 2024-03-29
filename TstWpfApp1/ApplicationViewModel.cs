﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Threading;
using System;
using System.Windows.Media;

namespace TstWpfApp1 {
	public class ApplicationViewModel : INotifyPropertyChanged {
		Timer stateTimer;
		public void CheckStatus(Object stateInfo) {
			Phone phone = selectedPhone;
			if (phone == null)
				Console.WriteLine("{0}", DateTime.Now.ToString("h:mm:ss.fff"));
			else {
				Console.WriteLine("{0} comp:{1}", DateTime.Now.ToString("h:mm:ss.fff"), phone.Company);
				SelectedPhone.Company = DateTime.Now.ToString("h:mm:ss.fff");
				TimeColor = (DateTime.Now.Second & 1) == 0 ? Brushes.Black : Brushes.Red;
				//Console.WriteLine("{0}", TimeColor);
				TimeText = " tt= " + DateTime.Now.ToString("h:mm:ss.fff");
			}
		}
		//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

		private Phone selectedPhone;
		private Brush timecolor;
		private string timetext;

		public ObservableCollection<Phone> Phones { get; set; }
		public Phone SelectedPhone {
			get { return selectedPhone; }
			set {
				selectedPhone = value;
				OnPropertyChanged("SelectedPhone");
			}
		} // /////////////////////////////////////////////////////////////////////////////////
		public Brush TimeColor {
			get { return timecolor; }
			set {
				timecolor = value;
				OnPropertyChanged("TimeColor");
			}
		} // /////////////////////////////////////////////////////////////////////////////////
		public string TimeText {
			get { return timetext; }
			set {
				timetext = value;
				OnPropertyChanged("TimeText");
			}
		} // /////////////////////////////////////////////////////////////////////////////////
		public ApplicationViewModel() {
			stateTimer = new Timer(CheckStatus, SelectedPhone, 1000, 1000);
			//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
			Phones = new ObservableCollection<Phone>
			{
				new Phone {Title="iPhone 7", Company="Apple", Price=56000 },
				new Phone {Title="Galaxy S7 Edge", Company="Samsung", Price =60000 },
				new Phone {Title="Elite x3", Company="HP", Price=56000 },
				new Phone {Title="Mi5S", Company="Xiaomi", Price=35000 }
			};
		}
		~ApplicationViewModel() {
			stateTimer.Dispose();
		} // ///////////////////////////////////////////////////////////////////////////////////
		//&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName]string prop = "") {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		} // /////////////////////////////////////////////////////////////////////////////////////////
	}
}
//  <TextBlock Text="{Binding Price, UpdateSourceTrigger=PropertyChanged}" Foreground="{Binding TimerColor, UpdateSourceTrigger=PropertyChanged}"  />