   M S B A M L   `   `   `  ����N  JAppPortariaControle, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AppPortariaControle.ViewsAppPortariaControle.Views  5   	   .    (AppPortariaControle.Views.ListarEmpresas   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:AppPortariaControle.Views     5   	     � Title$  Listar Empresas��5   	   $	��450��6!   $	��800��6.   $	��450��6:   $	��800��6J   $	��450��6Y   $	��800��6i    � WindowStartupLocation$ CenterScreen=�5	   	    � Icon$  /Imagens/LOGO_EMBAQUIM.jpg��5
   	   .��5      � .+�5   
   �� $#��Buscar uma empresa especifica��6   $
��Left=�68   $	��Top=�6S   $��	10,10,0,0q�6k   $��16(�6~   6�   �� 5   
   -   5         ��Name$ txtListEmpEsp��5      $
��Left=�6)   $	��Top=�6D   $	��500��6\   $��20��6h   $��	10,40,0,0q�6t   6�   �� 5   
   -   5        $��Buscar��5      $��70��6"   $��20��6-   $��Right=�69   $	��Top=�6U   $��
0,40,200,0q�6m   6�   �� 5   
   -   5        $��Listar Todas��5      $	��160��6(   $��20��64   $��Right=�6@   $	��Top=�6\   $��	0,40,10,0q�6t   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGridListarEmp��5         AutoGenerateColumns$ False��6.   $��10,112,10,10q�6J      
IsReadOnly$
 True��5         CanUserAddRows$ False��6      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Empresa��6%   
   
IsReadOnly$

 True��6R      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6d   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *	NomeEmp+6d   5      5      5      