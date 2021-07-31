using System.Collections.Generic;
using MenuAPI;
using vorpcharacter_cl.Utils;

namespace vorpcharacter_cl.Menus
{
    internal class MainMenu
    {
        private static readonly Menu mainMenu =
                new Menu(GetConfig.Langs["TitleMainMenu"], GetConfig.Langs["SubTitleMainMenu"]);

        private static bool setupDone;

        private static void SetupMenu()
        {
            if (setupDone)
            {
                return;
            }

            setupDone = true;
            MenuController.AddMenu(mainMenu);

            MenuController.EnableMenuToggleKeyOnController = false;
            MenuController.MenuToggleKey = 0;

            //SkinMenu
            MenuController.AddSubmenu(mainMenu, SkinMenu.GetMenu());

            var subMenuSkinBtn =
                    new MenuItem(GetConfig.Langs["TitleSkinMenu"], GetConfig.Langs["SubTitleSkinMenu"])
                    {
                            RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };

            mainMenu.AddMenuItem(subMenuSkinBtn);
            MenuController.BindMenuItem(mainMenu, SkinMenu.GetMenu(), subMenuSkinBtn);

            //FaceMenu
            MenuController.AddSubmenu(mainMenu, FaceMenu.GetMenu());

            var subMenuFaceBtn =
                    new MenuItem(GetConfig.Langs["TitleFaceMenu"], GetConfig.Langs["SubTitleFaceMenu"])
                    {
                            RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };

            mainMenu.AddMenuItem(subMenuFaceBtn);
            MenuController.BindMenuItem(mainMenu, FaceMenu.GetMenu(), subMenuFaceBtn);

            //ClothesMenu
            MenuController.AddSubmenu(mainMenu, ClothesMenu.GetMenu());

            var subMenuClothesBtn =
                    new MenuItem(GetConfig.Langs["TitleClothesMenu"], GetConfig.Langs["SubTitleClothesMenu"])
                    {
                            RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };

            mainMenu.AddMenuItem(subMenuClothesBtn);
            MenuController.BindMenuItem(mainMenu, ClothesMenu.GetMenu(), subMenuClothesBtn);

            var scaleValues = new List<string>();

            foreach (var sc in SkinsUtils.SCALE_LIST)
            {
                scaleValues.Add(GetConfig.Langs["Scale"] + sc);
            }

            var ScaleBtn =
                    new MenuListItem(GetConfig.Langs["ScaleList"], scaleValues, 4, GetConfig.Langs["ScaleDesc"])
                    {
                            RightIcon = MenuItem.Icon.STAR
                    };
            mainMenu.AddMenuItem(ScaleBtn);

            //Finish Button
            var FinishBtn =
                    new MenuItem(GetConfig.Langs["FinishBtnMainMenu"], GetConfig.Langs["SubFinishBtnMainMenu"])
                    {
                            RightIcon = MenuItem.Icon.TICK
                    };
            mainMenu.AddMenuItem(FinishBtn);

            //Events
            mainMenu.OnListIndexChange += (_menu, _listItem, _oldIndex, _newIndex, _itemIndex) =>
            {
                if (_itemIndex == 3)
                {
                    CreateCharacter.changeScale(SkinsUtils.SCALE_LIST[_newIndex]);
                }
            };

            mainMenu.OnMenuClose += _menu =>
            {
                if (CreateCharacter.isInCharCreation)
                {
                    CreateCharacter.CloseSecureMenu();
                }
            };

            mainMenu.OnItemSelect += (_menu, _item, _index) =>
            {
                // Code in here would get executed whenever an item is pressed.
                if (_index == 4)
                {
                    CreateCharacter.isInCharCreation = false;
                    CreateCharacter.SaveChanges();
                    mainMenu.CloseMenu();
                }
            };
        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return mainMenu;
        }
    }
}
