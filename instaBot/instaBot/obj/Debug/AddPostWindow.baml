   M S B A M L   `   `   `  ����C  ?instaBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:instaBotinstaBot  5   	       instaBot.AddPostWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:instaBot     5   	     � Title$  AddPostWindow��5   	   $��504.171��6   $	��452��60   ��5	      �� 5
   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFF2F8FD��6    � Offset$ 0Q�6-   � 5      $ 	#FFA4CDF0��6   $ 1Q�6-   5      .��5      � .+�5   
   �� -   5         ��Name$ 	imagePost��5      $	��174��6#   $��
80,10,81,0q�60   $	��Top=�6D   �� 5   
   -   5         ��Name$ label��5      $��Подпись:��6   $��27��62   $��80,278,81,0q�6>   $	��Top=�6S   �� 5   
   -   5         ��Name$ textBox_Caption��5      $��73��6+   $��80,310,81,0q�67    �� TextWrapping$
 Wrap=�6L   $	��Top=�6`   �� 5   
   -   5        	 ��Name$	 buttonReady��5      $��Готово��6&   $��114,411,115,0q�67   $	��Top=�6N   $��35��6f   $��18(�6r   ���	#FF63DA63$��Center=�6�   
 �� 	IsDefault
 . 6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ 
DateOfPost��5      $
��Left=�6)   $��114,197,0,0q�6D   $	��Top=�6Y   $��24��6q   $	��215��6}      SelectedDateFormat$
 Long=�6�      DisplayDate$ 05/18/2017 15:34:00��6�   �� 5   
   -   5         ��Name$ checkBoxTime��5      $;��5Подогнать под последний пост��6)   $
��Left=�6P   $��114,258,0,0q�6k   $	��Top=�6�   $	��197��6�    d� 	IsChecked$
 TrueY�6�   	� 5   
   -   5         	�Name$ radioButtonMorning��5      $��
Утром��62   $
��Left=�6B   $��114,235,0,0q�6]   $	��Top=�6r   	� 5   
   -   5        $ radioButtonEvening��5      $��Вечером��62   $
��Left=�6D   $��261,235,0,0q�6_   $	��Top=�6t   �� 5   
   -	   5        $	 buttonAbort��5      $��Отмена��6&   $
��Left=�67   $��
388,10,0,0q�6R   $	��Top=�6f   $��55��6~   ���	#FFDC53536�   5      5      