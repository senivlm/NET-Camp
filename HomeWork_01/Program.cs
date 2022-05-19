using NET_CAMP_HomeWork_s1_01;

Product prod = new Product("Item1", 12.3f, 33.4f);
Buy buy = new Buy(prod, 12);

Check.ShowAbout(prod);
Check.ShowAbout(buy);
buy.Volume = 1;
Check.ShowAbout(buy);
buy.Volume = -1;
Check.ShowAbout(buy);
