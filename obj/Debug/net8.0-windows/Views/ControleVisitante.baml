   M S B A M L   `   `   `  ����N  JAppPortariaControle, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AppPortariaControle.ViewsAppPortariaControle.Views  5   	   1    +AppPortariaControle.Views.ControleVisitante   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:AppPortariaControle.Views     5   	     � Title$  Controle Portaria��5   	   $
��1090��6$   $	��700��64   $��16(�6D   $	��700��6R   $
��1090��6b    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      � .+�5   
   � .+�5      � .+�5      �� $��Right=�6   $	��Top=�69   $��50��6Q   $��2��6]   $��
0,140,15,0q�6q   $	��270��6�   .+�5      �� -   5         ��Name$ BtnEntradaVisitantes��5      $��Entrada��6;   $	��130��6M   $��35��6Y   $
��Left=�6e   $
��Bold"�5      $��12(�6/   
������� � Cursor$
 Hand��6P   ���	#FF4CAF50���	#FF388E3C$��2q�5   L   $��0q�5      $��0q�6)    J� RenderTransformOrigin$ 0.5,0.5&�64    � 	Resources 5       �� 
TargetType �� Property �� Value	 �� 
TargetType
 ��Name �� CornerRadius �� Padding �� Triggers P� Property P� Value �� 
TargetName �� Triggers3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   ShadowDepth��%�  '��       �� 5      " ��6$   .�5   "   ��  � �� 5   &   �� 5   *   "	 ��6:   .R�5   .   �� $
 border��65   #��z��6E   #��z��5   5   #��z��5   5   $ 15��5   5   # z��5   5   ._�5   2   �� $��Center=�6C   $��Center=�6`   #��z��5    C   5!   /    5"   .   P� 5#   2    �@J�IsMouseOver$
 True��6Q   . �5$   6   �� $ border��6=    � �� �	#FF66BB6A6g   5%   3   5'   +   5)   #    5*   "   P� 5+   &    �@J�IsMouseOver$
 True��6E   . �5,   *   ��  � �� �	#FF4CAF506H   5-   '   P� 5.   &    �@J�IsMouseOver$
 True��6E   . �5/   *   ��  �@J�Effect 50   .     51   2   $ Black��6C   $ 10Q�6Q   $ 3Q�6a   53   +   54   '   56      57      z�58      V� 59      .~�5:   "   �� �� 5;   "    �� AngleX$ -1.373Q�60   �� 5<   "   T� 5=   "    T� X$ -0.599Q�65   5>      5@      �� 5C      -   5        $ BtnSaidaVisitantes��5C      $��Saída��69   $	��130��6I   $��35��6U   $	��Top=�6a   $��Right=�6y   $
��Bold"�5D      $��12(�6/   
�������$
 Hand��6P   ���	#FFF44336���	#FFD32F2F$��2q�5E   L   $��5q�5F      $��	0,-36,0,0q�6)    5G      ��%�  '��       �� 5H      " ��6$   .�5I   "   ��  � �� 5J   &   �� 5K   *   "	 ��6:   .R�5L   .   �� $
 border��65   #��z��6E   #��z��5M   5   #��z��5N   5   $ 15��5O   5   # z��5P   5   ._�5Q   2   �� $��Center=�6C   $��Center=�6`   #��z��5R   C   5S   /    5T   .   P� 5U   2    �@J�IsMouseOver$
 True��6Q   . �5V   6   �� $ border��6=    � �� �	#FFE573736g   5W   3   5Y   +   5[   #    5\   "   P� 5]   &    �@J�IsMouseOver$
 True��6E   . �5^   *   ��  � �� �	#FFF443366H   5_   '   P� 5`   &    �@J�IsMouseOver$
 True��6E   . �5a   *   ��  �@J�Effect 5b   .     5c   2   $ Black��6C   $ 10Q�6Q   $ 3Q�6a   5e   +   5f   '   5h      5i      5j      5k      �� 5n      -   5        $%��Cadastrar prestador de serviço��5n      $��Bottom=�6B   $��Center=�6]   $��Arial)�6z   $��0,0,0,97q�5o      $��16(�6+   $
��Bold"�69   $	��270��6b   $
 Hand��6�   ��6K   Z� 6�   �� 5p      -   5        $��Cadastrar Empresa��5p      $��Bottom=�65   $
��Left=�6P   $��Arial)�6k   $��	10,0,0,97q�5q      $��16(�6(   $
��Bold"�66   $	��200��6_   $
 Hand��6�   ��6H   Z� 6�   �� 5r      -   5        $!��Listar empresas cadastradas��5r      $��Bottom=�6?   $��Right=�6Z   $��Arial)�6v   $��0,0,5,97q�5s      $��16(�6'   $
��Bold"�65   $	��270��6^   $
 Hand��6�   ��6G   Z� 6�   ��5v      
� 5w       
� GradientOrigin$ 0.5,-0.2071&�6*    
� Center$ 0.5,0.5&�6G    
� RadiusX$	 0.5Q�6X    
� RadiusY$	 0.5Q�6f   .��5x      �  � Color$ 	#FF0CAFFF��6'    � Offset$	 0.0Q�69   � 5y      $ 	#FF0062A3��6'   $	 0.5Q�69   � 5z      $ 	#FF001F3D��6'   $	 1.0Q�69   5{      5}      � 5�      $��0,60,0,0q�6   .+�5�      �� $	��210��6   $��30��6$   $	��Top=�60   $
��Left=�6H   $��Inserir nome da Empresa��6c   $
��Bold"�5�      $��16(�6*   $��Arial)�68   
�������$��	20,91,0,0q�6^   �� 5�      -   5         ��Name$ TxbEmp��5�   e   $	��130��6   $��25��6&   $	��Top=�62   $
��Left=�6J   $��
235,94,0,0q�6u   $��16(�6�   �� 5�      -   5        $ BtnBuscarEmp��5�      $��70��6J   $��25��6U   $
 Hand��6a   $	��Top=�6o   $��Center=�6�   $��
0,94,270,0q�5�      $��0q�6-   $��Buscar��69   $��Arial)�6J   
���������5�      �� 5�       �� EndPoint$ 0.5,1&�6.    �� 
StartPoint$ 0.5,0&�6?   .��5�      � $ Black��6+   � 5�      $ 	#FF518CFF��6+   $ 1Q�6=   5�      5�      �� 5�      $��Controle Visitantes��6   $
��Left=�66   $��
385,17,0,0q�6Q   $	��Top=�6e   $��30(�6}   $
��Bold"�6�   5�      � 5�      $
��1080��6   $	��360��6    $��0,60,0,0q�6-   .+�5�      &   System.Windows.Controls.DataGrid  -   5           Name$  dataGridVisitantes��5�      !   AutoGenerateColumns$! False��67   $��	10,0,10,0q�6S   "   
IsReadOnly$
" True��5�      #   CanUserAddRows$# False��6-   $   Columns$ 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn%   Header$% Empresa��6-   &   
IsReadOnly$
& True��6Z   '   Width5  /System.Windows.Controls.DataGridLengthConverter$' * 6l   1  +System.Windows.Controls.DataGridBoundColumn(   Binding( 6>   �� *	NomeEmp+6l     5�      $
% CNPJ��6-   $
& True��6T   $	' 130 6f   ( 6;   �� *CNPJ+6f     5�      $ % Nome Prestador de Serviço��6-   $
& True��6m   $' * 6   ( 6P   �� *
NomeFunc+6     5�      $% Tipo Documento��6-   $
& True��6g   $	' 130 6y   ( 6E   �� *TipoDocumento+6y     5�      $% 	Documento��6-   $
& True��6^   $	' 100 6p   ( 6@   �� *	Documento+6p   5�      5�      5�      5�      5�      