   M S B A M L   `   `   `  ����C  ?instaBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:instaBotinstaBot  5   	       instaBot.InstaBotWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:instaBot     5   	   -   5          � Title$  InstaBotWindow��5   	   $��866.862��6    $��1348.771��61   $��0q�6B   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��150* �6   �� 5      $
��119* �6    � ColumnDefinitions 5   
   �� 5      $
��802* �6   �� 5      $
��539* �6   .+�5   
   �� -   5         ��Name$ 
textBoxLog��5      $��10q�6&   $��1��62    �� TextWrapping$
 Wrap=�6?   ���	#FFEADDDD$ �� HorizontalScrollBarVisibility$
 Auto=�6j    �� 
IsReadOnly . " �� VerticalScrollBarVisibility$
 Auto=�6�   ��. 6�   �� 5   
   
���   �$��0,0,0,1q�6%   $��0,0,0,10q�6?   ._�5      �� -   5         ��Name$ 
labelTimer��5      $�� ��6(   $��149,10,149,0q�63   $��36(�6I   $��Disabled=�6W   $��Center=�6�   $��Center=�6�   
������ 	 J� RenderTransformOrigin$	 0.5,0.5&�6�   $��Segoe WP Semibold)�6
  ���	#FF0070E0$	��108��6@  $	��Top=�6M  5      �� 5   
   -   5        
 ��Name$
 	imagePost��5      $	��300��6$   $	��230��60   $��178,109,169,155q�6=    J� OpacityMask �	#FFFBD6D66V   �� 5   
   -   5        $ textBoxCaption��5      $
 Wrap=�6*   $��52,343,46,24q�6>    . $��16(�6f   $��Center=�6t   $��Center=�6�   �� 5   
   -   5         ��Name$ listViewQueue��5      $��1��6*   $��2��6:   $��1,0,0,0q�6K   ���	#FFD5E3F7 J� 	AllowDrop . ��5      �� 5      .R�5      � -   5        $��2q�5       5      �� 5      �� 5      .+�5!      �� $	��150��69   $	��100��6E   $
��Fill=�6R   ��6    �� *Path+6R   �� 5"      $��1��6%   $��WrapWithOverflow=�6O   $��40��6o   $��12(�6~   $
��Left=�6�   $��5,0,4,0q�6�   $	��Top=�6�   ���	#FFFF0202��65   �� *	PosTime+��6�   Z� 6�   �� 5#      $��1��6%   $��WrapWithOverflow=�6O   $��40��6o   $��20(�6~   $
��Left=�6�   $��5,0,4,0q�6�   ��65   �� *	Caption+6�   5%      5&      ��5(      �� 5)       �� 
TargetType" �6   .�5*      ��  �� Property � �� �� Value$ 0,0,0,10q�6/   5+      5-      5/      50      