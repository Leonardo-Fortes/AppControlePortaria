   M S B A M L   `   `   `  ����N  JAppPortariaControle, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AppPortariaControle.ViewsAppPortariaControle.Views  5   	   *    $AppPortariaControle.Views.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:AppPortariaControle.Views     5   	     � Title$  Controle Portaria��5   	   $	��700��6#   $
��1074��60   $
��1074��6=   $	��700��6M   $	��700��6]   $
��1074��6m    � WindowStartupLocation$ CenterScreen=�5	   	   .��5      � .+�5   
   � ��5      
� 5       
� GradientOrigin$ 0.5,-0.2071&�6&    
� Center$ 0.5,0.5&�6C    
� RadiusX$	 0.5Q�6T    
� RadiusY$	 0.5Q�6b   .��5      �  � Color$ 	#FF0CAFFF��6#    � Offset$	 0.0Q�65   � 5      $ 	#FF0062A3��6#   $	 0.5Q�65   � 5      $ 	#FF001F3D��6#   $	 1.0Q�65   5      5      �� 5   
   $��\Imagens/logo2.png��6   $��Center=�5      $��Center=�5      $
��None=�5      $��
0,-424,0,0q�5      $
��1860��5      $	��862��5       J� RenderTransformOrigin$ 0.5,0.56&�5      z�5!      V� 5"      .~�5#      �� 	 �� ScaleX$		 0.2Q�6%   
 �� ScaleY$	
 0.2Q�62   �� 5$      �� 5%       �� Angle$ -0.396Q�6&   T� 5&      5'      5)      � 5,   
   .+�5-      �� $��Usuario:��6   $��80��6'   $��50��62   $��18(�6>   $
��Bold"�6L   $��Center=�6^   $��Center=�6�   $��0,200,345,0q�6�   �� 5.      $��Senha:��6   $��80��6%   $��50��60   $��18(�6<   $
��Bold"�6J   $��Center=�6\   $��Center=�6~   $��0,320,345,0q�6�   �� 5/      $��Center=�6   $��Center=�66   $��	0,260,0,0q�6Q   .+�51      �� ���	#1AFFFFFF���	#FF000000$��1q�53       �� CornerRadius$ 10��54       �� Padding$ 10q�55      $��0,0,0,10q�56      ._�57      ��  �� Orientation$ 
Horizontal=�6!   $��Center=�6:   .+�58      �� $��\Imagens/icon-user.png��6    $��24��59      $��24��5:      $��0,0,10,0q�5;      �� 5<      -   5         ��Name$ 
txtUsuario��5<   "   $	��200��66   $��30��6B   $
��Bold"�5=      $��16(�5>      
�������
������ $��0q�5A      $��Center=�5B      $��0q�5C      5D      5E      �� 5F      ���	#1AFFFFFF���	#FF000000$��1q�5H      $ 10��5I      $ 10q�5J      ._�5K      �� $ 
Horizontal=�6!   $��Center=�6:   .+�5L      �� $��\Imagens/icone-senha.png��6    $��24��5M      $��24��5N      $��0,0,10,0q�5O      G� 5P      -   5         G�Name$ txtSenha��5P   &   $	��200��69   $��30��6E   $
��Bold"�5Q      $��16(�5R      
�������
������ $��0q�5U      $��Center=�5V      $��0q�5W      5X      5Y      5Z      �� 5\      -   5         ��Name$ 	btnEntrar��5\      $��Entrar��6(   $	��140��69   $��40��6E   $��Center=�5]   	   $��Center=�6&   $
��Bold"�5^   	   $��16(�6   
���   �$��Arial)�5_      $��	0,480,0,0q�6/    �� 	IsDefault$
 True��5`   	   ���	#1AFFFFFF���	#FF000000$��1q�5a   8   $��10q�5b   	    � Cursor$
 Hand��6    � 	Resources 5c       �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value �� 
TargetName �� Triggers��%:  '��       �� 5d      " ��6   .�5e      ��  � �� 5f      �� 5g   "   " ��62   .R�5h   &   �� $ border��6-   #��z��6=   #��z��5i   !   #��z��5j   !   $ 10��5k   !   # z��5l   !   ._�5m   *   �� $��Center=�6;   $��Center=�6X   #��z��5n   /   5o   '    5p   &   P� 5r   *    �@J�IsMouseOver$
 True��6I   . �5s   .   �� $ border��65    � �� �	#FFCCCCCC6_   5t   +   5v   #   5x       5y      P� 5{       �@J�IsMouseOver$
 True��6=   . �5|   "   ��  � �� �	#FFAAAAAA6@   5}      5      5�      5�      5�      5�      5�      