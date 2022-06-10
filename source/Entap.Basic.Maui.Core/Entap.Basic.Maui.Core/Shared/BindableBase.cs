using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Entap.Basic.Maui.Core
{
	public class BindableBase : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// プロパティ値を変更し、クライアントに通知し、プロパティの変更有無を返す。
        /// </summary>
        /// <returns><c>true</c>, プロパティに変更あり<c>false</c>プロパティに変更なし</returns>
        /// <typeparam name="T">プロパティの型。</typeparam>
        /// <param name="storage">get アクセス操作子と set アクセス操作子両方を使用したプロパティへの参照。</param>
        /// <param name="value">プロパティに必要な値。</param>
        /// <param name="propertyName">リスナーに通知するために使用するプロパティの名前。省略可能。</param>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

