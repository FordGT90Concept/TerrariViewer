using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.IO;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Reflection;

namespace TerrariViewer.TerrariaObjects
{
    public class Player : INotifyPropertyChanged
    {
        private const int CurrentRelease = 94;

        private const int ItemLength = 2289;

        #region Properties

        private string name = "Name";
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private byte difficulty = 0;
        public byte Difficulty
        {
            get { return difficulty; }
            set
            {
                difficulty = value;
                OnPropertyChanged("Difficulty");
            }
        }

        private bool gender = true;
        public bool Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public const int HairMax = 35;
        private int hair = 0;
        public int Hair
        {
            get { return hair; }
            set
            {
                hair = value;
                if (hair < 0)
                {
                    hair = HairMax;
                }
                else if (hair > HairMax)
                {
                    hair = 0;
                }
                OnPropertyChanged("Hair");
            }
        }



        private byte hairDye;

        public byte HairDye

        {

            get { return hairDye; }

            set

            {

                hairDye = value;

                OnPropertyChanged("HairDye");

            }

        }



        private byte hideVisual;

        public byte HideVisual

        {

            get { return hideVisual; }

            set

            {

                hideVisual = value;

                OnPropertyChanged("HideVisual");

            }

        }

        private int currentHealth = 200;
        public int CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                currentHealth = value;
                OnPropertyChanged("CurrentHealth");
            }
        }

        private int maximumHealth = 200;
        public int MaximumHealth
        {
            get { return maximumHealth; }
            set
            {
                maximumHealth = value;
                if (maximumHealth < 0)
                {
                    maximumHealth = 0;
                }
                OnPropertyChanged("MaximumHealth");

                if (currentHealth > maximumHealth)
                {
                    CurrentHealth = maximumHealth;
                }
            }
        }

        private int currentMana = 0;
        public int CurrentMana
        {
            get { return currentMana; }
            set
            {
                currentMana = value;
                OnPropertyChanged("CurrentMana");
            }
        }

        private int maximumMana = 0;
        public int MaximumMana
        {
            get { return maximumMana; }
            set
            {
                maximumMana = value;
                if (maximumMana < 0)
                {
                    maximumMana = 0;
                }
                OnPropertyChanged("MaximumMana");
                if (currentMana > maximumMana)
                {
                    CurrentMana = maximumMana;
                }
            }
        }

        private Color hairColor = Color.FromRgb(215, 90, 55);
        public Color HairColor
        {
            get { return hairColor; }
            set
            {
                hairColor = value;
                OnPropertyChanged("HairColor");
            }
        }

        private Color skinColor = Color.FromRgb(255, 125, 90);
        public Color SkinColor
        {
            get { return skinColor; }
            set
            {
                skinColor = value;
                OnPropertyChanged("SkinColor");
            }
        }

        private Color eyeColor = Color.FromRgb(105, 90, 75);
        public Color EyeColor
        {
            get { return eyeColor; }
            set
            {
                eyeColor = value;
                OnPropertyChanged("EyeColor");
            }
        }

        private Color shirtColor = Color.FromRgb(175, 165, 140);
        public Color ShirtColor
        {
            get { return shirtColor; }
            set
            {
                shirtColor = value;
                OnPropertyChanged("ShirtColor");
            }
        }

        private Color underShirtColor = Color.FromRgb(160, 180, 215);
        public Color UnderShirtColor
        {
            get { return underShirtColor; }
            set
            {
                underShirtColor = value;
                OnPropertyChanged("UnderShirtColor");
            }
        }

        private Color pantsColor = Color.FromRgb(255, 230, 175);
        public Color PantsColor
        {
            get { return pantsColor; }
            set
            {
                pantsColor = value;
                OnPropertyChanged("PantsColor");
            }
        }

        private Color shoeColor = Color.FromRgb(160, 105, 60);
        public Color ShoeColor
        {
            get { return shoeColor; }
            set
            {
                shoeColor = value;
                OnPropertyChanged("ShoeColor");
            }
        }

        public Item[] Armor { get; set; }
        public Item[] Accessories { get; set; }
        public Item[] Vanity { get; set; }
        public Item[] Dye { get; set; }
        public Item[] Inventory { get; set; }
        public Item[] Coins { get; set; }
        public Item[] Ammo { get; set; }
        public Item[] Bank { get; set; }
        public Item[] Safe { get; set; }
        public Buff[] Buffs { get; set; }
        public int[] spX { get; set; }
        public int[] spY { get; set; }
        public int[] spI { get; set; }
        public string[] spN { get; set; }

        private bool hotbarLocked = false;
        public bool HotbarLocked
        {
            get { return hotbarLocked; }
            set
            {
                hotbarLocked = value;
                OnPropertyChanged("HotbarLocked");
            }
        }

        #endregion

        public Player()
        {
            Armor = new Item[3];
            for (int i = 0; i < Armor.Length; i++)
            {
                Armor[i] = new Item();
            }

            Accessories = new Item[5];
            for (int i = 0; i < Accessories.Length; i++)
            {
                Accessories[i] = new Item();
            }

            Vanity = new Item[8];
            for (int i = 0; i < Vanity.Length; i++)
            {
                Vanity[i] = new Item();
            }

            Dye = new Item[8];
            for (int i = 0; i < Dye.Length; i++)
            {
                Dye[i] = new Item();
            }

            Inventory = new Item[50];
            for (int i = 0; i < Inventory.Length; i++)
            {
                Inventory[i] = new Item();
            }

            Coins = new Item[4];
            for (int i = 0; i < Coins.Length; i++)
            {
                Coins[i] = new Item();
            }

            Ammo = new Item[4];
            for (int i = 0; i < Ammo.Length; i++)
            {
                Ammo[i] = new Item();
            }

            Bank = new Item[40];
            for (int i = 0; i < Bank.Length; i++)
            {
                Bank[i] = new Item();
            }

            Safe = new Item[40];
            for (int i = 0; i < Safe.Length; i++)
            {
                Safe[i] = new Item();
            }

            Buffs = new Buff[22];
            for (int i = 0; i < Buffs.Length; i++)
            {
                Buffs[i] = new Buff();
            }

            spX = new int[200];
            spY = new int[200];
            spI = new int[200];
            spN = new string[200]; 
            for (int i = 0; i < 200; i++)
            {
                spN[i] = "";
            }
        }

        public void Load(string path)
        {
            bool decryptionCheck;
#if !DEBUG
            try
            {
#endif
                string outputFile = path + ".dat";
                decryptionCheck = DecryptFile(path, outputFile);

                if (!decryptionCheck)
                {
                    using (FileStream stream = new FileStream(outputFile, FileMode.Open))
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        int release = reader.ReadInt32();
                        Console.WriteLine("Release: " + release.ToString());
                        Name = reader.ReadString();

                        if (release >= 10)
                        {
                            if (release >= 17)
                            {
                                Difficulty = reader.ReadByte();
                            }
                            else if (reader.ReadBoolean())
                            {
                                Difficulty = 2;
                            }
                        }



                        Hair = reader.ReadInt32();

                        if (release >= 82)

                            HairDye = reader.ReadByte();

                        if (release >= 83)

                            HideVisual = reader.ReadByte();
                        if (release <= 17)
                        {
                            if (Hair == 5 || Hair == 6 || Hair == 9 || Hair == 11)
                                Gender = false; // Female
                            else
                                Gender = true; // Male
                        }
                        else
                            Gender = reader.ReadBoolean();

                        CurrentHealth = reader.ReadInt32();
                        MaximumHealth = reader.ReadInt32();
                        CurrentMana = reader.ReadInt32();
                        MaximumMana = reader.ReadInt32();

                        hairColor.R = reader.ReadByte();
                        hairColor.G = reader.ReadByte();
                        hairColor.B = reader.ReadByte();

                        skinColor.R = reader.ReadByte();
                        skinColor.G = reader.ReadByte();
                        skinColor.B = reader.ReadByte();

                        eyeColor.R = reader.ReadByte();
                        eyeColor.G = reader.ReadByte();
                        eyeColor.B = reader.ReadByte();

                        shirtColor.R = reader.ReadByte();
                        shirtColor.G = reader.ReadByte();
                        shirtColor.B = reader.ReadByte();

                        underShirtColor.R = reader.ReadByte();
                        underShirtColor.G = reader.ReadByte();
                        underShirtColor.B = reader.ReadByte();

                        pantsColor.R = reader.ReadByte();
                        pantsColor.G = reader.ReadByte();
                        pantsColor.B = reader.ReadByte();

                        shoeColor.R = reader.ReadByte();
                        shoeColor.G = reader.ReadByte();
                        shoeColor.B = reader.ReadByte();

                        if (release >= 38)
                        {
                            for (int i = 0; i < Armor.Length; i++)
                            {
                                Armor[i].SetFromID(reader.ReadInt32());
                                Armor[i].Prefix = reader.ReadByte();
                            }

                            for (int i = 0; i < Accessories.Length; i++)
                            {
                                Accessories[i].SetFromID(reader.ReadInt32());
                                Accessories[i].Prefix = reader.ReadByte();
                            }



                            int adjust = 3; // Old had 3 Vanity/Dyes, new has 8.

                            if (release >= 81)

                                adjust = Vanity.Length;

                            for (int i = 0; i < adjust; i++)

                            {

                                Vanity[i].SetFromID(reader.ReadInt32());

                                Vanity[i].Prefix = reader.ReadByte();

                            }



                            if (release >= 47)

                            {

                                adjust = 3;  // Old had 3 Vanity/Dyes, new has 8.

                                if (release >= 81)

                                    adjust = Dye.Length;

                                for (int i = 0; i < adjust; i++)

                                {

                                    Dye[i].SetFromID(reader.ReadInt32());

                                    Dye[i].Prefix = reader.ReadByte();

                                }

                            }

                            if (release >= 58)
                            {
                                for (int i = 0; i < Inventory.Length; i++)
                                {
                                    int id = reader.ReadInt32();
                                    if (id >= ItemLength)
                                    {
                                        Inventory[i].SetFromID(0);
                                    }
                                    else
                                    {
                                        Inventory[i].SetFromID(id);
                                        Inventory[i].StackSize = reader.ReadInt32();
                                        Inventory[i].Prefix = reader.ReadByte();
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < 40; i++)
                                {
                                    int id = reader.ReadInt32();
                                    if (id >= ItemLength)
                                    {
                                        Inventory[i].SetFromID(0);
                                    }
                                    else
                                    {
                                        Inventory[i].SetFromID(id);
                                        Inventory[i].StackSize = reader.ReadInt32();
                                        Inventory[i].Prefix = reader.ReadByte();
                                    }
                                }
                            }

                            for (int i = 0; i < Coins.Length; i++)
                            {
                                int id = reader.ReadInt32();
                                if (id >= ItemLength)
                                {
                                    Coins[i].SetFromID(0);
                                }
                                else
                                {
                                    Coins[i].SetFromID(id);
                                    Coins[i].StackSize = reader.ReadInt32();
                                    Coins[i].Prefix = reader.ReadByte();
                                }
                            }

                            for (int i = 0; i < Ammo.Length; i++)
                            {
                                int id = reader.ReadInt32();
                                if (id >= ItemLength)
                                {
                                    Ammo[i].SetFromID(0);
                                }
                                else
                                {
                                    Ammo[i].SetFromID(id);
                                    Ammo[i].StackSize = reader.ReadInt32();
                                    Ammo[i].Prefix = reader.ReadByte();
                                }
                            }

                            if (release >= 58)
                            {
                                for (int i = 0; i < Bank.Length; i++)
                                {
                                    Bank[i].SetFromID(reader.ReadInt32());
                                    Bank[i].StackSize = reader.ReadInt32();
                                    Bank[i].Prefix = reader.ReadByte();
                                }
                                for (int i = 0; i < Safe.Length; i++)
                                {
                                    Safe[i].SetFromID(reader.ReadInt32());
                                    Safe[i].StackSize = reader.ReadInt32();
                                    Safe[i].Prefix = reader.ReadByte();
                                }
                            }
                            else
                            {
                                for (int i = 0; i < 20; i++)
                                {
                                    Bank[i].SetFromID(reader.ReadInt32());
                                    Bank[i].StackSize = reader.ReadInt32();
                                    Bank[i].Prefix = reader.ReadByte();
                                }
                                for (int i = 0; i < 20; i++)
                                {
                                    Safe[i].SetFromID(reader.ReadInt32());
                                    Safe[i].StackSize = reader.ReadInt32();
                                    Safe[i].Prefix = reader.ReadByte();
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < Armor.Length; i++)
                            {
                                Armor[i].SetFromName(reader.ReadString());
                                if (release >= 36)
                                {
                                    Armor[i].Prefix = reader.ReadByte();
                                }
                            }

                            for (int i = 0; i < Accessories.Length; i++)
                            {
                                Accessories[i].SetFromName(reader.ReadString());
                                if (release >= 36)
                                {
                                    Accessories[i].Prefix = reader.ReadByte();
                                }
                            }

                            if (release >= 6)
                            {
                                for (int i = 0; i < Vanity.Length; i++)
                                {
                                    Vanity[i].SetFromName(reader.ReadString());
                                    if (release >= 36)
                                    {
                                        Vanity[i].Prefix = reader.ReadByte();
                                    }
                                }
                            }

                            for (int i = 0; i < 40; i++)
                            {
                                Inventory[i].SetFromName(reader.ReadString());
                                Inventory[i].StackSize = reader.ReadInt32();
                                if (release >= 36)
                                {
                                    Inventory[i].Prefix = reader.ReadByte();
                                }
                            }

                            for (int i = 0; i < Coins.Length; i++)
                            {
                                Coins[i].SetFromName(reader.ReadString());
                                Coins[i].StackSize = reader.ReadInt32();
                                if (release >= 36)
                                {
                                    Coins[i].Prefix = reader.ReadByte();
                                }
                            }

                            if (release >= 15)
                            {
                                for (int i = 0; i < Ammo.Length; i++)
                                {
                                    Ammo[i].SetFromName(reader.ReadString());
                                    Ammo[i].StackSize = reader.ReadInt32();
                                    if (release >= 36)
                                    {
                                        Ammo[i].Prefix = reader.ReadByte();
                                    }
                                }
                            }

                            for (int i = 0; i < 20; i++)
                            {
                                Bank[i].SetFromName(reader.ReadString());
                                Bank[i].StackSize = reader.ReadInt32();
                                if (release >= 36)
                                {
                                    Bank[i].Prefix = reader.ReadByte();
                                }
                            }

                            if (release >= 20)
                            {
                                for (int i = 0; i < 20; i++)
                                {
                                    Safe[i].SetFromName(reader.ReadString());
                                    Safe[i].StackSize = reader.ReadInt32();
                                    if (release >= 36)
                                    {
                                        Safe[i].Prefix = reader.ReadByte();
                                    }
                                }
                            }
                        }

                        if (release >= 11)

                        {

                            int adjust = Buffs.Length;

                            if (release < 74)

                                adjust = 10;

                            for (int i = 0; i < adjust; i++)

                                Buffs[i].SetFromID(reader.ReadInt32(), reader.ReadInt32());
                        }

                        for (int i = 0; i < 200; i++)
                        {
                            int num = reader.ReadInt32();
                            if (num == -1)
                                break;
                            spX[i] = num;
                            spY[i] = reader.ReadInt32();
                            spI[i] = reader.ReadInt32();
                            spN[i] = reader.ReadString();
                        }

                        if (release >= 16)
                            HotbarLocked = reader.ReadBoolean();
                        reader.Close();

                    }

#if !DEBUG
            File.Delete(outputFile);
#endif
                    OnPropertyChanged(null);
                }
#if !DEBUG
            }
            catch
            {
                return;
            }
#endif
        }

        public void Save(string path)
        {
            string destFileName = path + ".bak";
            if (File.Exists(path))
                File.Copy(path, destFileName, true);

            string tempFile = path + ".dat";
            using (FileStream stream = new FileStream(tempFile, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(stream)) 
            {
                writer.Write(CurrentRelease);
                writer.Write(Name);
                writer.Write(Difficulty);
                writer.Write(Hair);

                writer.Write(HairDye);

                writer.Write(HideVisual);
                writer.Write(Gender);
                writer.Write(CurrentHealth);
                writer.Write(MaximumHealth);
                writer.Write(CurrentMana);
                writer.Write(MaximumMana);
                writer.Write(HairColor.R);
                writer.Write(HairColor.G);
                writer.Write(HairColor.B);
                writer.Write(SkinColor.R);
                writer.Write(SkinColor.G);
                writer.Write(SkinColor.B);
                writer.Write(EyeColor.R);
                writer.Write(EyeColor.G);
                writer.Write(EyeColor.B);
                writer.Write(ShirtColor.R);
                writer.Write(ShirtColor.G);
                writer.Write(ShirtColor.B);
                writer.Write(UnderShirtColor.R);
                writer.Write(UnderShirtColor.G);
                writer.Write(UnderShirtColor.B);
                writer.Write(PantsColor.R);
                writer.Write(PantsColor.G);
                writer.Write(PantsColor.B);
                writer.Write(ShoeColor.R);
                writer.Write(ShoeColor.G);
                writer.Write(ShoeColor.B);

                for (int i = 0; i < Armor.Length; i++)
                {
                    if (Armor[i].Name == null)
                        Armor[i].Name = "";
                    writer.Write(Armor[i].Id);
                    writer.Write((byte)Armor[i].Prefix);
                }

                for (int i = 0; i < Accessories.Length; i++)
                {
                    if (Accessories[i].Name == null)
                        Accessories[i].Name = "";
                    writer.Write(Accessories[i].Id);
                    writer.Write((byte)Accessories[i].Prefix);
                }

                for (int i = 0; i < Vanity.Length; i++)
                {
                    if (Vanity[i].Name == null)
                        Vanity[i].Name = "";
                    writer.Write(Vanity[i].Id);
                    writer.Write((byte)Vanity[i].Prefix);
                }

                for (int i = 0; i < Dye.Length; i++)
                {
                    if (Dye[i].Name == null)
                        Dye[i].Name = "";
                    writer.Write(Dye[i].Id);
                    writer.Write((byte)Dye[i].Prefix);
                }

                for (int i = 0; i < Inventory.Length; i++)
                {
                    if (Inventory[i].Name == null)
                        Inventory[i].Name = "";
                    writer.Write(Inventory[i].Id);
                    writer.Write(Inventory[i].StackSize);
                    writer.Write((byte)Inventory[i].Prefix);
                }

                for (int i = 0; i < Coins.Length; i++)
                {
                    if (Coins[i].Name == null)
                        Coins[i].Name = "";
                    writer.Write(Coins[i].Id);
                    writer.Write(Coins[i].StackSize);
                    writer.Write((byte)Coins[i].Prefix);
                }

                for (int i = 0; i < Ammo.Length; i++)
                {
                    if (Ammo[i].Name == null)
                        Ammo[i].Name = "";
                    writer.Write(Ammo[i].Id);
                    writer.Write(Ammo[i].StackSize);
                    writer.Write((byte)Ammo[i].Prefix);
                }

                for (int i = 0; i < Bank.Length; i++)
                {
                    if (Bank[i].Name == null)
                        Bank[i].Name = "";
                    writer.Write(Bank[i].Id);
                    writer.Write(Bank[i].StackSize);
                    writer.Write((byte)Bank[i].Prefix);
                }

                for (int i = 0; i < Safe.Length; i++)
                {
                    if (Safe[i].Name == null)
                        Safe[i].Name = "";
                    writer.Write(Safe[i].Id);
                    writer.Write(Safe[i].StackSize);
                    writer.Write((byte)Safe[i].Prefix);
                }

                for (int i = 0; i < Buffs.Length; i++)
                {
                    writer.Write(Buffs[i].Id);
                    writer.Write(Buffs[i].GameDuration);
                }

                for (int i = 0; i < 200; i++)
                {
                    if (spN[i] == String.Empty)
                    {
                        writer.Write(-1);
                        break;
                    }

                    writer.Write(spX[i]);
                    writer.Write(spY[i]);
                    writer.Write(spI[i]);
                    writer.Write(spN[i]);
                }
                writer.Write(HotbarLocked);
                writer.Close();
            }
            Player.EncryptFile(tempFile, path);
#if !DEBUG
            File.Delete(tempFile);
#endif
            
        }

        #region Decrypt / Encrypt File

        private const string EncryptDecryptKey = "h3y_gUyZ";

        /// <summary>
        /// Decrypts a file
        /// </summary>
        /// <param name="InputFile">File to decrypt</param>
        /// <param name="OutputFile">Decrypted file path</param>
        /// <returns>true if successful, else false</returns>
        private static bool DecryptFile(string InputFile, string OutputFile)
        {
            string key = "h3y_gUyZ";
            byte[] bytes = new UnicodeEncoding().GetBytes(key);
            FileStream fileStream1 = new FileStream(InputFile, FileMode.Open);
            RijndaelManaged managed = new RijndaelManaged();
            CryptoStream cryptoStream = new CryptoStream(fileStream1, managed.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            FileStream fileStream2 = new FileStream(OutputFile, FileMode.Create);
            try
            {
                int num;
                while ((num = cryptoStream.ReadByte()) != -1)
                {
                    fileStream2.WriteByte((byte)num);
                }
                fileStream2.Close();
                cryptoStream.Close();
                fileStream1.Close();
            }
            catch
            {
                fileStream2.Close();
                fileStream1.Close();
                File.Delete(OutputFile);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Encrypts a file
        /// </summary>
        /// <param name="InputFile">File to encrypt</param>
        /// <param name="OutputFile">Encrypted file path</param>
        private static void EncryptFile(string InputFile, string OutputFile)
        {
            byte[] key = new UnicodeEncoding().GetBytes(EncryptDecryptKey);

            try
            {
                using (FileStream fileOut = new FileStream(OutputFile, FileMode.Create))
                using (RijndaelManaged encryptor = new RijndaelManaged())
                using (CryptoStream cStream = new CryptoStream(fileOut, encryptor.CreateEncryptor(key, key), CryptoStreamMode.Write))
                using (FileStream fileIn = new FileStream(InputFile, FileMode.Open))
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;

                    while ((bytesRead = fileIn.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        #endregion

        #region PropertyChanged

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        #endregion
    }
}
