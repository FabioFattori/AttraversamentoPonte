   M S B A M L   `   `   `  ����I  EprogettoStrada, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:progettoStradaprogettoStrada  5   	       progettoStrada.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:progettoStrada     5   	     � Title$  
MainWindow��5   	   $	��450��6   $��1127.023��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��73* �6   �� 5      $	��11* �6   .+�5   
   �� $
��Left=�6   $	��478��6+   $��67,135,-14.2,-193q�68   $	��Top=�6S   $
��1068��6k   $��primo schizzo ponte.png��6x   $��2��6�   �� 5   
   $
��Left=�6   $	��182��6,   $��-290,137,0,0q�69   $	��Top=�6O   $	��389��6g   $��restoDelPonte.png��6s   �� 5   
   -   5         ��Name$! SpawnMacchinaInStradaDestra��5   �   $��inserisci
macchina��6   $
��Left=�62   $��73��6M   $��
830,57,0,0q�6Y   $	��Top=�6m   $	��113��6�   6�   �� 5   
   -   5        $# SpawnMacchinaInStradaSinistra��5   �   $��inserisci
macchina��6   $
��Left=�62   $��73��6M   $��
285,62,0,0q�6Y   $	��Top=�6m   $	��113��6�   6�   �� 5   
   -   5         ��Name$ Immagine_Nuova_Sinistra��5   p   $
��Left=�6   $��72��6+   $��-280,173,0,0q�67   $	��Top=�6M   $��80��6e   $��modelloMacchina2.png��6�   �� 5   
   -   5        $ Immagine_Nuova_Destra��5   u   $
��Left=�6   $��53��6+   $��1194,192,-148.2,0q�67   $	��Top=�6R   $��75��6j   $��modelloMacchina4.png��6�   �� 5   
   -   5        $ Immagine_Sinistra_0��5   p   $
��Left=�6   $��67��6+   $��-195,225,0,0q�67   $	��Top=�6M   $��80��6e   $��modelloMacchina3.png��6�   �� 5   
   -   5        $ Immagine_Sinistra_1��5   p   $
��Left=�6   $��72��6+   $��-195,173,0,0q�67   $	��Top=�6M   $��80��6e   $��modelloMacchina2.png��6�   �� 5   
   -   5        $ Immagine_Sinistra_2��5   p   $
��Left=�6   $��70��6+   $��-110,225,0,0q�67   $	��Top=�6M   $��80��6e   $��modelloMacchina3.png��6�   �� 5   
   -   5        $ Immagine_Sinistra_3��5   p   $
��Left=�6   $��67��6+   $��-110,178,0,0q�67   $	��Top=�6M   $��80��6e   $��modelloMacchina2.png��6�   �� 5   
   -	   5        $ Immagine_Sinistra_4��5   p   $
��Left=�6   $��67��6+   $��-275,225,0,0q�67   $	��Top=�6M   $��75��6e   $��modelloMacchina3.png��6�   �� 5   
   -
   5        $ Immagine_Destra_0��5   v   $
��Left=�6   $��60��6+   $��1274,137,-261.2,0q�67   $	��Top=�6R   $	��108��6j   $��modelloMacchina1.png��6�   �� 5   
   -   5        $ Immagine_Destra_1��5   u   $
��Left=�6   $��53��6+   $��1274,192,-228.2,0q�67   $	��Top=�6R   $��75��6j   $��modelloMacchina4.png��6�   �� 5   
   -   5        $ Immagine_Destra_2��5   v   $
��Left=�6   $��60��6+   $��1119,137,-111.2,0q�67   $	��Top=�6R   $	��113��6j   $��modelloMacchina1.png��6�   �� 5   
   -   5        $ Immagine_Destra_3��5   t   $
��Left=�6   $��53��6+   $��1119,192,-73.2,0q�67   $	��Top=�6Q   $��75��6i   $��modelloMacchina4.png��6�   �� 5   
   -   5        $ Immagine_Destra_4��5   v   $
��Left=�6   $��60��6+   $��1194,137,-173.2,0q�67   $	��Top=�6R   $	��100��6j   $��modelloMacchina1.png��6�    J� RenderTransformOrigin$ 0.537,0.541&�6�   �� 5    
   -   5         ��Name$% numeroMacchineDaCreareASinistra��5    r   $
��Left=�6   $��32��6.   $��
403,80,0,0q�6:   $	��Top=�6N   $	��161��6f   6r   �� 5!   
   -   5        $# numeroMacchineDaCreareADestra��5!   r   $
��Left=�6   $��32��6.   $��
948,80,0,0q�6:   $	��Top=�6N   $	��161��6f   6r   �� 5"   
   $ ��numero macchine da creare:��6   $
��Left=�65   $��34��6P   $��
403,41,0,0q�6\   $	��Top=�6p   $	��161��6�   �� 5#   
   $ ��numero macchine da creare:��6   $
��Left=�65   $��34��6P   $��
948,46,0,0q�6\   $	��Top=�6p   $	��161��6�   5&      5'      