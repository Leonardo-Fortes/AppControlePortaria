   M S B A M L   `   `   `  ����N  JAppPortariaControle, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AppPortariaControle.ViewsAppPortariaControle.Views  5   	   0    *AppPortariaControle.Views.AdicionarVeiculo   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:AppPortariaControle.Views     5   	     � Title$  Adicionar Veiculo��5   	   $	��265��6#   $	��265��64   $	��600��6D   $	��600��6S    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      � $	��205��6   $	��205��6   .+�5   
   �� $��Placa��6   $	��Top=�6    $
��Left=�68   $��16(�6S   $��10,0,0,0q�6a   �� 5   
   -   5         ��Name$ txtPlaca��5      $	��100��6$   $
��Left=�60   $��16(�6L    �� TextWrapping$
 Wrap=�6Z   $	��Top=�6o   $��60,7,0,0q�6�   �� 5   
   $
��Tipo��6   $	��Top=�6   $
��Left=�67   $��	229,1,0,0q�6R   $��16(�6e   6v   �� 5   
   -   5        $��comboBoxVeiculo��5      $	��100��6*   $��23��66   $	��Top=�6B   $
��Left=�6Z   $��	275,6,0,0q�6u    �� SelectedItem$" comboBoxVeiculo_selectedItem��6�   .@�5      �� $��Carro��6   6-   �� 5      $
��Moto��6   6,   5      �� 5   
   $��Modelo��6   $	��Top=�6!   $��Right=�69   $��16(�6U   $��	0,0,122,0q�6c   6x   �� 5   
   -   5        $ 	txtModelo��5      $	��100��6%   $��16(�61   $��23��6?   $	��Top=�6K   $��Right=�6c   $��0,7,10,0q�6   6�   �� 5   
   $��Nome Funcionário��6   $��16(�6+   $	��Top=�69   $
��Left=�6Q   $��	10,44,0,0q�6l   6�   �� 5   
   -   5        $ txtNomeFunc��5      $	��420��6'   $��16(�63   $��23��6A   $	��Top=�6M   $��Right=�6e   $��	0,48,10,0q�6�   6�   �� 5   
   -   5        $��20��5      $��20��6   $�� ��6(   $	��Top=�63   $��Right=�6K   $��	0,49,12,0q�6   $��8(�6�    � Cursor$
 Hand��6�   ��5      �� 5       �� ImageSource$  /Imagens/icon-pesquisa.png��6    j� Stretch$ Uniform=�6F   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGridFuncionários��5      	   AutoGenerateColumns$	 False��61   $	��100��6M   $	��570��6Z   $��Bottom=�6f   $��Center=�6�   $��
10,0,10,29q�6�   
   
IsReadOnly$

 True��5         HeadersVisibility$
 None=�6      Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly$
 True��6C      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *	Motorista+6U   5#      �� 5$   
   -   5        $��	Adicionar��5$      $��60��6%   $��20��60   $��Bottom=�6<   $��Right=�6W   $��0,0,10,0q�6s   $
 Hand��6�   6�   5(      5)      