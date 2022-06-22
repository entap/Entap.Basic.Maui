# Entap.Basic.Maui.Core
* [PageManager](#pagemanager)  
 


## PageManager
ViewModelからのページ遷移をサポートする。

> **Warning**
> * TabbedPageから画面遷移が必要場合は、TabbedPageのChildrenをNavigatioｎPageとすることを推奨する。  
> https://docs.microsoft.com/ja-jp/dotnet/maui/user-interface/pages/tabbedpage#navigate-within-a-tab
> Entap.Basic.Forms.PageManagerでは、SetNavigationMainPageにTabbedPageで遷移が可能であったが  
> maui-android（6.0.300）において、Navigation Barのみ表示され、Childrenに指定したPageが表示されない不具合が発生。  
> （戻るボタンタップ後、アプリを再表示すると画面が表示されることがある）  
  
### 使用説明  
* ページ遷移制御は、デフォルトでは[PageNavigation](/source/Entap.Basic.Maui.Core/Entap.Basic.Maui.Core/Shared/PageManager/PageNavigation/PageNavigation.cs)をするが、[PageManager.SetNavigation()](/source/Entap.Basic.Maui.Core/Entap.Basic.Maui.Core/Shared/PageManager/PageManager.cs#L24)で任意の処理の指定が可能。　　
[PageNavigation](/source/Entap.Basic.Maui.Core/Entap.Basic.Maui.Core/Shared/PageManager/PageNavigation/PageNavigation.cs)の以下のメソッドはオーバライド可能　
    * CreateNavigationPage  
    * CreateClosableNavigationPage  
それ以外の処理を書き換えたい場合は、[IPageNavigation](/source/Entap.Basic.Maui.Core/Entap.Basic.Maui.Core/Shared/PageManager/PageNavigation/IPageNavigation.cs)を継承した処理を実装し指定する。

  
* TabbedPage使用時は、TabbedPageNavigationBehaviorを指定することで  
タブを切り替え時に、PageViewModelのOnEntry(), OnExit()が実行されるようになる。
```xml
<TabbedPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:basic="http://entap.co.jp/schemas/basic"
             x:Class="Sample.TabbedNavigationPage"
             x:DataType="local:TabbedNavigationPageViewModel">
    <TabbedPage.Behaviors>
        <basic:TabbedPageNavigationBehavior/>
    </TabbedPage.Behaviors>
```
