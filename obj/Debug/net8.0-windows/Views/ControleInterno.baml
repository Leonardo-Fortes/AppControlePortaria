   M S B A M L   `   `   `  ����N  JAppPortariaControle, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AppPortariaControle.ViewsAppPortariaControle.Views  5   	   /    )AppPortariaControle.Views.ControleInterno   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:AppPortariaControle.Views     5   	     � Title$  Controle Portaria��5   	   $
��1074��6$   $	��700��64   $	��700��6D   $
��1074��6T   $��16(�6d    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      � ��5   
   
� 5       
� GradientOrigin$ 0.5,-0.2071&�6"    
� Center$ 0.5,0.5&�6?    
� RadiusX$	 0.5Q�6P    
� RadiusY$	 0.5Q�6^   .��5      �  � Color$ 	#FF0CAFFF��6    � Offset$	 0.0Q�61   � 5      $ 	#FF0062A3��6   $	 0.5Q�61   � 5      $ 	#FF001F3D��6   $	 1.0Q�61   5      .+�5   
   � .+�5      �� $��Controle Interno��6   $
��Left=�6/   $��
385,17,0,0q�6J   $	��Top=�6^   $��30(�6v   $
��Bold"�6�   � 5      $	��540��6   $	��100��6   $	��Top=�6,   $
��Left=�6D   $��	10,90,0,0q�6_   .+�5      �� $	��205��6   $��30��6$   $��Center=�60   $
��Left=�6K   $ ��Digite a placa do Veiculo:��6f   $
��Bold"�6�   $��16(�5      $��Arial)�6&   
�������$��0,0,0,0q�6L   6`   �� 5      -   5         ��Name$ TxbPlaca��5   j   $	��130��6   $��25��6&   $��Center=�62   $��Center=�6M   $��0,0,0,0q�6|   $��16(�6�   6�   �� 5      -   5        	 ��Name$	 	btnBuscar��5      $��70��6D   $��28��6O   
 � Cursor$

 Hand��6[   $��Center=�6i   $��Right=�6�   $��	0,0,120,0q�6�   $��0q�6�   $��Buscar��6�   $��Arial)�6�   
���������5      �� 5       �� EndPoint$ 0.5,1&�6.    �� 
StartPoint$ 0.5,0&�6?   .��5      � $ Black��6+   � 5      $ 	#FF518CFF��6+   $ 1Q�6=   5      5      5!      � 5"       � ColumnDefinitions 5#      �� 5$      $
��329* �6'   �� 5%      $	��71* �6'   5)      � 5*      .+�5+      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridVeiculos��5+      $
��1080��65   $	��360��6B      AutoGenerateColumns$ False��6O   $��Stretch=�6k   $��Stretch=�6�   $��
10,10,10,0q�6�      
IsReadOnly$
 True��5,         Columns 5-      0  *System.Windows.Controls.DataGridTextColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Header$ Placa��6-      
IsReadOnly$
 True��6V      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6h   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *Placa+6h     5/      $
 Tipo��6-   $
 True��6T   $ * 6f    6;   �� *Tipo+6f     50      $ Modelo��6-   $
 True��6X   $ * 6j    6=   �� *Modelo+6j     51      $ 	Motorista��6-   $
 True��6^   $ * 6p    6@   �� *	Motorista+6p   53      �� 54      $��Right=�6   $	��Top=�69   $	��150��6Q   $��2��6^   $��	0,90,10,0q�6r   .+�56      �� -   5        $	 
BtnEntrada��56      $��Entrada��61   $	��130��6C   $��35��6O   $
��Bold"�57      $��12(�6   
�������$

 Hand��69   ���	#FF4CAF50���	#FF388E3C$��2q�58   5   $��0q�59      $��125,28,0,20q�6    J� RenderTransformOrigin$ 0.5,0.5&�6'    � 	Resources 5:       �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius �� Padding �� Triggers  P� Property! P� Value" �� 
TargetName# �� Triggers3  -System.Windows.Media.Effects.DropShadowEffect$   Color%   
BlurRadius&   ShadowDepth��%�  '��       �� 5;      " ��6$   .�5<   "   ��  � �� 5=   &   �� 5>   *   " ��6:   .R�5?   .   �� $ border��65   #��z��6E   #��z��5@      #��z��5A      $ 15��5B      # z��5C      ._�5D   2   �� $��Center=�6C   $��Center=�6`   #��z��5E   ,   5F   /    5G   .   P� 5I   2     �@J�IsMouseOver$
! True��6Q   . �5J   6   �� $" border��6=    � �� �	#FF66BB6A6g   5K   3   5M   +   5O   #   # 5P   "   P� 5R   &     �@J�IsMouseOver$
! True��6E   . �5S   *   ��  � �� �	#FF4CAF506H   5T   '   P� 5V   &     �@J�IsMouseOver$
! True��6E   . �5W   *   ��  �@J�Effect 5X   .     5Y   2   $$ Black��6C   $% 10Q�6Q   $& 3Q�6a   5[   +   5\   '   5^      5_      z�5`      V� 5a      .~�5b   "   �� �� 5c   "   ' �� AngleX$' -1.373Q�60   �� 5d   "   T� 5e   "   ( T� X$( -0.599Q�65   5f      5h      �� 5m      -   5        $	 BtnSaida��5m      $��Saída��6/   $	��130��6?   $��35��6K   $��Center=�6W   $��Center=�6r   $
��Bold"�5n      $��12(�6,   
�������$

 Hand��6M   ���	#FFF44336���	#FFD32F2F$��2q�5o   I   $��5q�5p      $��400,-76,0,0q�6&    5q      ��%�  '��       �� 5r      " ��6$   .�5s   "   ��  � �� 5t   &   �� 5u   *   " ��6:   .R�5v   .   �� $ border��65   #��z��6E   #��z��5w      #��z��5x      $ 15��5y      # z��5z      ._�5{   2   �� $��Center=�6C   $��Center=�6`   #��z��5|   ,   5}   /    5~   .   P� 5�   2     �@J�IsMouseOver$
! True��6Q   . �5�   6   �� $" border��6=    � �� �	#FFE573736g   5�   3   5�   +   5�   #   # 5�   "   P� 5�   &     �@J�IsMouseOver$
! True��6E   . �5�   *   ��  � �� �	#FFF443366H   5�   '   P� 5�   &     �@J�IsMouseOver$
! True��6E   . �5�   *   ��  �@J�Effect 5�   .     5�   2   $$ Black��6C   $% 10Q�6Q   $& 3Q�6a   5�   +   5�   '   5�      5�      5�      5�      �� 5�      -   5        $��Cadastrar Veiculo��5�      $��Bottom=�65   $
��Left=�6P   $��Arial)�6k   $��
10,0,0,127q�5�      $��16(�6   $
��Bold"�6'   $	��200��6P   $

 Hand��6{   ��69   Z� 6{   5�      5�      5�      5�      