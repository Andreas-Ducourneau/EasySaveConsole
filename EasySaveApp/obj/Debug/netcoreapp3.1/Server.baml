   M S B A M L   `   `   `  ����F  BEasySaveApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:EasySaveAppEasySaveApp  5   	       EasySaveApp.Server   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:EasySaveApp     5   	     � Title$  Server��5   	   $	��450��6   $	��800��6%   .��5	      � .+�5
   
   �� -   5         ��Name$ txtIP��5
      $
��Left=�6!   $��	69,27,0,0q�6<   $��127.0.0.1:1302��6O    �� TextWrapping$
 Wrap=�6e   $	��Top=�6y   $	��419��6�   �� 5   
   -   5         ��Name$ btnStart��5      $��Start��6#   $
��Left=�63   $��406,315,0,0q�6N   $	��Top=�6c   $��82��6{   6�   �� 5   
   $��	Server : ��6   $
��Left=�6$   $��	17,23,0,0q�6?   $	��Top=�6R    J� RenderTransformOrigin$ 0.798,0.656&�6j   �� 5   
   -   5        $ 
txtMessage��5      $
��Left=�6&   $��
69,291,0,0q�6A   $
 Wrap=�6U   $	��Top=�6i   $	��419��6�   $��19��6�   6�   �� 5   
   -   5        $ btnSend��5      $
��Send��6"   $
��Left=�61   $��309,315,0,0q�6L   $	��Top=�6a   $��82��6y   �� 5   
   -   5        $ txtInfo��5      $
��Left=�6#   $��	69,66,0,0q�6>   $
 Wrap=�6Q   $	��Top=�6e   $	��419��6}   $	��203��6�   $ �� HorizontalScrollBarVisibility$ Hidden=�6�   �� 5   
   $��	Message :��6   $
��Left=�6$   $��	6,288,0,0q�6?   $	��Top=�6R   $ 0.798,0.656&�6j   �� 5   
   -   5         ��Name$ 	lstClient��5      $��504,66,27,16q�6%   .@�5      �� 5      �� 5   
   $��Client IP : ��6   $
��Left=�6'   $��
504,23,0,0q�6B   $	��Top=�6V   $ 0.798,0.656&�6n   �� 5   
   -   5        $
��Stop��5      $
��Left=�6    $��406,357,0,0q�6;   $	��Top=�6P   $��82��6h   6s   5      5      