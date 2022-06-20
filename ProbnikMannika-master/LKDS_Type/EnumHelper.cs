using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace LKDS_Type
{
    public static class EnumHelper
    {
        public static string GetNameOfEnum(this Enum enumVal)
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            try
            {
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? ((DescriptionAttribute)attributes[0]).Description : "";
            }
            catch
            {
                return Convert.ToInt16(enumVal).ToString("X4");
            }

        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Union16
        {
            [FieldOffset(0)]
            public Int16 Value;
            [NonSerialized]
            [FieldOffset(0)]
            public UInt16 UValue;
            [FieldOffset(0)]
            public byte Byte0;
            [FieldOffset(1)]
            public byte Byte1;
            public bool isBitSet(byte index)
            {
                if (index >= 15)
                    return false;
                return (Value & (1L << index)) != 0;
            }
            [System.Xml.Serialization.XmlIgnore]
            public byte[] ToArray => new byte[]
            {
            Byte0,
            Byte1
            };
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Union32
        {
            [FieldOffset(0)]
            public Int32 Value;
            [NonSerialized]
            [FieldOffset(0)]
            public UInt32 UValue;
            [FieldOffset(0)]
            public byte Byte0;
            [FieldOffset(1)]
            public byte Byte1;
            [FieldOffset(2)]
            public byte Byte2;
            [FieldOffset(3)]
            public byte Byte3;
            public bool isBitSet(byte index)
            {
                if (index >= 31)
                    return false;
                return (Value & (1L << index)) != 0;
            }
            [System.Xml.Serialization.XmlIgnore]
            public byte[] ToArray => new byte[]
            {
            Byte0,
            Byte1,
            Byte2,
            Byte3
            };
        }
        public enum CmdTypes
        {
            AdapterOn = 0x05,
            AdapterOff = 0x04,
            AdapterReset = 0x01,
            LiftOrder = 0x01,
            LiftCallUpSideA = 0x02,
            LiftCallDownSideA = 0x03,
            LiftCallUpSideB = 0x04,
            LiftCallDownSideB = 0x05,
            LiftLockOrder = 0x11,
            LiftLockUpSideA = 0x12,
            LiftLockDownSideA = 0x13,
            LiftLockUpSideB = 0x14,
            LiftLockDownSideB = 0x15,
            LiftUnLockOrder = 0x21,
            LiftUnLockUpSideA = 0x22,
            LiftUnLockDownSideA = 0x23,
            LiftUnLockUpSideB = 0x24,
            LiftUnLockDownSideB = 0x25

        }
        public enum CAN_Devices
        {
            [Description("ЛБ/Концентратор")]
            LB = 0,
            [Description("USB - VOICE converter")]
            USB = 2,
            [Description("VRP - VIDEO converter")]
            VRP = 3,
            [Description("Переговорное устройство v7(ПУv7)")]
            PU = 4,
            [Description("Переговорное устройство этажное v7(ЭПУv7)")]
            EPU = 5,
            [Description("Удлинитель WiFi v7")]
            wifi = 6,
            [Description("Адаптер входов v7(АСК-16)")]
            ASK = 7,
            [Description("Адаптер ТУ v7(АТУ-8*2)")]
            ATU = 8,
            [Description("Адаптер ПУ v7(АПУ-1)")]
            APU = 9,
            [Description("Адаптер Последовательного Интерфейса(АПИ-1)")]
            API = 10,
            [Description("Портал Контроллер Доступа (ПКД2*2)")]
            PKD22 = 11,
            [Description("Портал Контроллер Доступа (ПКД2*16)")]
            PKD216 = 12,
            [Description("Переговорное устр. Аккум. платформы (ПУ АП)")]
            PUAP = 13,
            [Description("Адаптер релейных выходов (АРВ-8*6)")]
            ARV = 14,
            [Description("Адаптер Лампа Индикаторная (АЛИ-1)")]
            ALI = 15,
            [Description("Адаптер Токовых Сигналов (АТС-4*4)")]
            ATS = 16,
            [Description("Адаптер ModBUS (АМБ-1)")]
            AMB = 17,
            [Description("Адаптер Звукового Оповещения (АЗО-1)")]
            AZO = 18,
            [Description("Адаптер Переговорного Устройства 2 (АПУ-2)")]
            APU2 = 19,
            [Description("Переговорное устройство посадоч. площ.(ПУ ПП)")]
            PUPP = 20,
            [Description("Выносной Модуль Управления (ВМУ)")]
            VMU = 21,
            [Description("Адаптер шлейфов (АШЛ-6*4)")]
            ASHL = 22,
            [Description("Портал Контроллер Доступа (ПКД1*2)")]
            PKD12 = 24,
            [Description("ПКД2*2-режим Команд")]
            PKD22CR = 25,
            [Description("ПКД2*16-режим Команд")]
            PKD216CR = 26,
            [Description("ПКД2*1-режим Команд")]
            PKD21CR = 27,
            [Description("Тип не определен")]
            notype = 255,
        }
        public enum Device_Status
        {
            [Description("Инициализация-нет данных")]
            ninitialization = 0,
            [Description("Ожидание готовности устройства-нет жанных")]
            nwaiting_readiness = 1,
            [Description("Ожидание данных устройства-нет данных")]
            waiting_data = 2,
            [Description("Данные устарели-не обновлялись более 30 сек")]
            data_outdated = 3,
            [Description("Данные актуальны-подлежат интерпретации")]
            data_actual = 4,
            [Description("Обнаружен рестарт устройства-нет данных")]
            restart = 5,
        }
        public enum Key_Format
        {
            [Description("Неизвестный формат ключа")]
            unknown_format = 0x00,
            [Description("Формат ключа Dallas")]
            dallas = 0x01,
            [Description("EM-Marine")]
            em_marine = 0x02,
            [Description("EM-Marine (ручной ввод)")]
            em_marine_manual_input = 0x03,
            [Description("Mifare Classic")]
            mifare_classic = 0x04,
            [Description("Mifare Classic (ручной ввод)")]
            mifare_classic_manual_input = 0x05,
            [Description("Mifare 1K")]
            mifare_1K = 0x06,
            [Description("Mifare 1K (ручной ввод)")]
            mifare_1K_manual_input = 0x07,
            [Description("Mifare 4K")]
            mifare_4K = 0x08,
            [Description("Mifare 4K (ручной ввод)")]
            mifare_4K_manual_input = 0x09,
            [Description("Mifare UltraLight")]
            mifare_ultra_light = 0x0A,
            [Description("Mifare UltraLight (ручной ввод)")]
            mifare_ultra_light_manual_input = 0x0B,
            [Description("HID ProxCard II")]
            HID_Prox_Card_II = 0x0C,
            [Description("HID ProxCard II (ручной ввод)")]
            HID_Prox_Card_II_manual_input = 0x0D,
            [Description("клавиатура")]
            keyboard = 0x0E,
            [Description("RF-433тип 1")]
            rf = 0x0F,
            [Description("Wiegand - абстрактный ключ, полученный в формате Wiegand")]
            wiegand = 0x10,
            [Description("кнопка (открывания)")]
            opening_button = 0x11,
        }



        ///

        [StructLayout(LayoutKind.Explicit)]
        public struct Union64
        {
            [FieldOffset(0)]
            public Int64 Value;
            [NonSerialized]
            [FieldOffset(0)]
            public UInt64 UValue;
            [FieldOffset(0)]
            public byte Byte0;
            [FieldOffset(1)]
            public byte Byte1;
            [FieldOffset(2)]
            public byte Byte2;
            [FieldOffset(3)]
            public byte Byte3;
            [FieldOffset(4)]
            public byte Byte4;
            [FieldOffset(5)]
            public byte Byte5;
            [FieldOffset(6)]
            public byte Byte6;
            [FieldOffset(7)]
            public byte Byte7;

            public bool isBitSet(byte index)
            {
                if (index >= 63)
                    return false;
                return (Value & (1L << index)) != 0;
            }
            [System.Xml.Serialization.XmlIgnore]
            public byte[] ToArray => new byte[]
            {
            Byte0,
            Byte1,
            Byte2,
            Byte3,
            Byte4,
            Byte5,
            Byte6,
            Byte7
            };
        }

        public static bool isStateFlag(LiftStatus cur, ulong val)
        {
            return (val & (ulong)cur) != 0;
        }

        [Description("Тип данных - Тип подустройства")]
        public enum LBClassType
        {
            [Description("Загрузчик")]
            te6Load = 0,        // Загрузчик.
            [Description("Могилевлифтмаш (УУЛ)")]
            te6Mog = 1,     // Могилевлифтмаш (УУЛ)
            [Description("МЭЛ (СУЛ1)")]
            te6MEL = 2,     // МЭЛ (СУЛ1)
            [Description("МППЛ (НПО «Вектор»)")]
            te6MPPL = 3,        // МППЛ (НПО «Вектор»)
            [Description("ОТИС")]
            te6Otis = 4,        // ОТИС
            [Description("ШУЛК17")]
            te6S17 = 5,     // ШУЛК17
            [Description("ШУЛК32")]
            te6S32 = 6,     // ШУЛК32
            [Description("УКЛ/УЛ")]
            te6UCL = 7,     // УКЛ/УЛ
            [Description("Релейный лифт")]
            te6Rele = 8,        // Релейный лифт. 
            [Description("SODIMAS")]
            te6Sodi = 9,        // SODIMAS
            [Description("LG DI")]
            te6LGDI = 10,       // LG_DI
            [Description("LG DSS")]
            te6LGDSS = 11,      // LG_DSS
            [Description("эскалатор")]
            teEscal = 12,       // эскалатор
            [Description("AXEL")]
            te6AXEL = 13,       // AXEL
            [Description("ELEX")]
            te6ELEX = 14,       // ELEX
            [Description("СПУЛ")]
            te6SPUL = 15,       // СПУЛ
            [Description("THYSSEN")]
            te6THYSSEN = 16,        // THYSSEN
            [Description("ОЛИМП")]
            te6OLIMP = 17,      // ОЛИМП
            [Description("Doppler")]
            te6Doppler = 18,        // Doppler
            [Description("THYSSEN (LS3)")]
            te6THYSSEN3 = 19,       // THYSSEN (LS3)
            [Description("EXPRESS")]
            te6EXPRESS = 20,        // EXPRESS
            [Description("BLT")]
            te6BLT = 21,        // BLT
            [Description("THYSSEN (TAC50)")]
            te6THYS_50 = 22,        // THYSSEN (TAC50)
            [Description("LISA (KLEEMANN)")]
            te6LISA = 23,       // LISA (KLEEMANN)
            [Description("Schindler")]
            te6Schind = 24,     // Schindler
            [Description("PLK (программируемые логические контроллеры)")]
            te6PLK = 25,        // PLK (программируемые логические контроллеры)
            [Description("УЭЛР")]
            te6UELR = 26,       // УЭЛР
            [Description("THYSSEN (MCI)")]
            te6THYSMCI = 27,        // THYSSEN (MCI)
            [Description("ORONA")]
            te6ORONA = 28,      // ORONA
            [Description("ARKEL")]
            te6ARKEL = 29,      // ARKEL 
            [Description("BG15")]
            te6BG15 = 30,       // BG15
            [Description("МППЛ (НПО «Вектор») версия 2")]
            te6MPPL2 = 31,      // МППЛ (НПО «Вектор») версия 2           
            [Description("VEGA")]
            te6VEGA = 32,       // VEGA            
            [Description("Sodimas QI")]
            te6SodQI = 33,      // Sodimas QI           
            [Description("ТР")]
            te6TR = 34,     // ТР            
            [Description("DMG")]
            te6DMG = 35,        // DMG           
            [Description("MIK_EL")]
            te6MIK_EL = 36,     // MIK_EL            
            [Description("KONE")]
            te6KONE = 37,       // KONE           
            [Description("S9")]
            te6S9 = 38,     // S9           
            [Description("HYUNDAI")]
            te6HYUNDAI = 39,        // HYUNDAI            
            [Description("NICE")]
            te6NICE = 40,       // NICE            
            [Description("VASSLER")]
            te6VASSLER = 41,        // VASSLER            
            [Description("FST2")]
            te6FST2 = 42,       // FST2            
            [Description("CANNY")]
            te6CANNY = 43,      // CANNY
            [Description("SILVER")]
            te6SILVER = 44,     // SILVER            
            [Description("ИНВАЛИДНЫЙ")]
            te6Invalid = 45,        // ИНВАЛИДНЫЙ            
            [Description("BETACONTROL")]
            te6BETACTRL = 46,       // BETACONTROL            
            [Description("StaGeHELLAS")]
            te6StaGeHEL = 47,       // StaGeHELLAS
            [Description("ШК6000")]
            te6SK6000 = 48,     // ШК6000
            [Description("ТКЛ")]
            te6TKL = 49,        // ТКЛ
            [Description("СУЛ2010")]
            te6SUL2010 = 50,        // СУЛ2010
            [Description("Союз")]
            te6Union = 51,      // Союз
            [Description("ШЛ-Р")]
            te6SH_R = 52,       // ШЛ-Р
            [Description("FT9X0")]
            te6FT9X0 = 53,      // FT9X0
            [Description("LLC")]
            te6LLC = 54,        // LLC
            [Description("KOLLMORGEN")]
            te6KOLLMORGEN = 55,     // KOLLMORGEN
            [Description("SECURLIFT")]
            te6SECURLIFT = 56,      // SECURLIFT
            [Description("IMEM")]
            te6IMEM = 57,       // IMEM
            [Description("WIPO")]
            te6WIPO = 58,       // WIPO
            [Description("KLST (VESTNER)")]
            te6VESTNER = 59,        // KLST (VESTNER)
            [Description("CARLOS SILVA")]
            te6CARLOS_SILVA = 60,   // CARLOS SILVA
            [Description("KOYO")]
            te6KOYO = 61,       // KOYO
            [Description("BL6")]
            te6BL6 = 62,        // BL6
            [Description("WEBER")]
            te6WEBER = 63,      // WEBER
            [Description("iAStar")]
            te6iAStar = 64,     // iAStar
            [Description("GTE")]
            te6GTE = 65,        // GTE
            [Description("AUTINOR")]
            te6AUTINOR = 66,        // AUTINOR
            [Description("THYSCMC")]
            te6THYSCMC = 67,        // THYSCMC
            [Description("DAESUNG")]
            te6DAESUNG = 68,        // DAESUNG
            [Description("AS380")]
            te6AS380 = 69,      // AS380
            [Description("ECL1")]
            te6ECL1 = 70,       // ECL1
            [Description("ECLIPSE")]
            te6ECLIPSE = 71,        // ECLIPSE
            [Description("SKG")]
            te6SKG = 72,        // SKG
            [Description("ML65X")]
            te6ML65X = 73,      // ML65X
            [Description("ML60X")]
            te6ML60X = 74,      // ML60X
            [Description("PDAHL")]
            te6PDAHL = 75,      // PDAHL
            [Description("УЛЖМ")]
            te6ULGM = 76,       // УЛЖМ
            [Description("ISLv4")]
            te6ISLv4 = 77,      // ISLv4
            [Description("ML50")]
            te6ML50 = 78,       // ML50
            [Description("ViaSerie")]
            teViaSerie = 79,        // ViaSerie
            [Description("MASHIBA")]
            te6MASHIBA = 80,        // MASHIBA
            [Description("HYUNDAI_CAN")]
            te6HYUN_CAN = 81,       // HYUNDAI_CAN
            [Description("BST (B08 China)")]
            te6BST = 82,        // BST (B08 China)
            [Description("KST (NewLift Germany)")]
            te6KST = 83,        // KST (NewLift Germany)
            [Description("MITSUBISHI")]
            te6MITSUBI = 84,        // MITSUBISHI
            [Description("АСУД Концентратор")]
            te6ASUD = 85,       // АСУД Концентратор
            [Description("Hidral")]
            te6Hidral = 86,     // Hidral
            [Description("Smart controller")]
            te6Smart = 87,      // Smart controller
            [Description("EASY TRONIC")]
            te6EASY = 88,       // EASY TRONIC
            [Description("GILAN")]
            te6GILAN = 89,      // GILAN
            [Description("INVT")]
            te6INVT = 90,       // INVT
            [Description("SUMEL")]
            te6SUMEL = 91,      // SUMEL
            [Description("GMV")]
            te6GMV = 92,        // GMV
            [Description("МЛК")]
            te6MLK = 93,        // МЛК
            [Description("TRAVIS")]
            te6TRAVIS = 94,     // TRAVIS
            [Description("УКЛ-40Р (Распределенная станция МПУ-2)")]
            te6UKL40R = 95,     // УКЛ-40Р (Распределенная станция МПУ-2)
            [Description("DIGILIFT (Gruppomillepiani)")]
            te6DIGILIFT = 97,    // DIGILIFT (Gruppomillepiani)
            [Description("KONE_ESC(EMB - 501)")]
            teKoneEsc = 98,    // KONE_ESC(EMB - 501)
            [Description("KRONA")]
            teKrona = 99,    // KRONA
            [Description("ELMI")]
            teElmi = 100,    //ELMI
            [Description("HD ONE(Hedefsan)")]
            tehdOne = 101,    // HD ONE(Hedefsan)
            [Description("SODIMAS QI TOUCH(CAN)")]
            teSodimasQiTouch = 102,    // SODIMAS QI TOUCH(CAN)           
            [Description("MASHIBA MS68")]
            teMashibaMs68 = 103,    // MASHIBA MS68            
            [Description("BP306")]
            teBp306 = 104,    // BP306           
            [Description("NP1(HISSTEMA)")]
            te6Np1 = 105,    // NP1(HISSTEMA)            
            [Description("HIDRA CRONO(Carlos Silva)")]
            teHidraCrono = 106,    //HIDRA CRONO(Carlos Silva)            
            [Description("MC3000")]
            teMc3000 = 107,    // MC3000            
            [Description("MLC01")]
            teMlc01 = 108,    // MLC01            
            [Description("THYSSEN GEC")]
            teThyssenGec = 109,    // THYSSEN GEC           
            [Description("K-TYPE(Mitsubishi ЭСК)")]
            teKType = 110,      // K-TYPE(Mitsubishi ЭСК)    
            [Description("GPS(Mitsubishi)")]
            teGPS = 111,
            [Description("BR100 (MOVILIFT)")]
            teBR100 = 112,
            [Description("MODEUN")]
            teMODEUN = 113,
            [Description("GUANGRI")]
            teGUANGRI = 114,
            [Description("KONE KCE")]
            teKONEKCE = 115,
            [Description("IFE")]
            teIFE = 116,
            [Description("EAGLE (IGV)")]
            teEAGLE = 117,
            [Description("RODOS (UKR)")]
            teRODOS = 118,
            [Description("SISTEL")]
            teSISTEL = 119,
            [Description("CIBES A5")]
            teCIBES = 120,
            [Description("121")]
            te121 = 121,
            [Description("122")]
            te122 = 122,
            [Description("123")]
            te123 = 123,
            [Description("124")]
            te124 = 124,
            [Description("125")]
            te125 = 125,
            [Description("126")]
            te126 = 126,
            [Description("127")]
            te127 = 127,
            [Description("128")]
            te128 = 128,
            [Description("129")]
            te129 = 129,
            [Description("130")]
            te130 = 130,
        }

        public enum Stage
        {
            [Description("Вызов из МП")]
            callMP = 0,
            [Description("Вызов из приямка")]
            callPR = 253,
            [Description("Вызов из кабины")]
            callKAB = 254,
            [Description("Вызова нет")]
            NOcall = 255,
            [Description("Вызов с 1 этажа")]
            call1 = 251,
            [Description("Вызов с 2 этажа")]
            call2 = 250,
            [Description("Вызов с 3 этажа")]
            call3 = 249,
            [Description("Вызов с 4 этажа")]
            call4 = 248,
            [Description("Вызов с 5 этажа")]
            call5 = 247,
            [Description("Вызов с 6 этажа")]
            call6 = 246,
            [Description("Вызов с 7 этажа")]
            call7 = 245,
            [Description("Вызов с 8 этажа")]
            call8 = 244,
            [Description("Вызов с 9 этажа")]
            call9 = 243,
            [Description("Вызов с 10 этажа")]
            call10 = 242,
            [Description("Вызов с 11 этажа")]
            call11 = 241,
            [Description("Вызов с 12 этажа")]
            call12 = 240,
            [Description("Вызов с 13 этажа")]
            call13 = 239,
            [Description("Вызов с 14 этажа")]
            call14 = 238,
            [Description("Вызов с 15 этажа")]
            call15 = 237,
            [Description("Вызов с 16 этажа")]
            call16 = 236,
            [Description("Вызов с 17 этажа")]
            call17 = 235,
            [Description("Вызов с 18 этажа")]
            call18 = 234,
            [Description("Вызов с 19 этажа")]
            call19 = 233,
            [Description("Вызов с 20 этажа")]
            call20 = 232,
            [Description("Вызов с 21 этажа")]
            call21 = 231,
            [Description("Вызов с 22 этажа")]
            call22 = 230,
            [Description("Вызов с 23 этажа")]
            call23 = 229,
            [Description("Вызов с 24 этажа")]
            call24 = 228,
            [Description("Вызов с 25 этажа")]
            call25 = 227,
            [Description("Вызов с 26 этажа")]
            call26 = 226,
            [Description("Вызов с 27 этажа")]
            call27 = 225,
            [Description("Вызов с 28 этажа")]
            call28 = 224,
            [Description("Вызов с 29 этажа")]
            call29 = 223,
            [Description("Вызов с 30 этажа")]
            call30 = 222,
            [Description("Вызов с 31 этажа")]
            call31 = 221,
            [Description("Вызов с 32 этажа")]
            call32 = 220,
            [Description("Вызов с 33 этажа")]
            call33 = 219,
            [Description("Вызов с 34 этажа")]
            call34 = 218,
            [Description("Вызов с 35 этажа")]
            call35 = 217,
            [Description("Вызов с 36 этажа")]
            call36 = 216,
            [Description("Вызов с 37 этажа")]
            call37 = 215,
            [Description("Вызов с 38 этажа")]
            call38 = 214,
            [Description("Вызов с 39 этажа")]
            call39 = 213,
            [Description("Вызов с 40 этажа")]
            call40 = 212,
            [Description("Вызов с 41 этажа")]
            call41 = 211,
            [Description("Вызов с 42 этажа")]
            call42 = 210,
            [Description("Вызов с 43 этажа")]
            call43 = 209,
            [Description("Вызов с 44 этажа")]
            call44 = 208,
            [Description("Вызов с 45 этажа")]
            call45 = 207,
            [Description("Вызов с 46 этажа")]
            call46 = 206,
            [Description("Вызов с 47 этажа")]
            call47 = 205,
            [Description("Вызов с 48 этажа")]
            call48 = 204,
            [Description("Вызов с 49 этажа")]
            call49 = 203,
            [Description("Вызов с 50 этажа")]
            call50 = 202,
            [Description("Вызов с 51 этажа")]
            call51 = 201,
            [Description("Вызов с 52 этажа")]
            call52 = 200,
            [Description("Вызов с 53 этажа")]
            call53 = 199,
            [Description("Вызов с 54 этажа")]
            call54 = 198,
            [Description("Вызов с 55 этажа")]
            call55 = 197,
            [Description("Вызов с 56 этажа")]
            call56 = 196,
            [Description("Вызов с 57 этажа")]
            call57 = 195,
            [Description("Вызов с 58 этажа")]
            call58 = 194,
            [Description("Вызов с 59 этажа")]
            call59 = 193,
            [Description("Вызов с 60 этажа")]
            call60 = 192,
            [Description("Вызов с 61 этажа")]
            call61 = 191,
            [Description("Вызов с 62 этажа")]
            call62 = 190,
            [Description("Вызов с 63 этажа")]
            call63 = 189,
            [Description("Вызов с 64 этажа")]
            call64 = 188,
        }

        public enum WorkMode
        {
            [Description("Нормальный режим")]
            normal = 0,
            [Description("Погрузка")]
            upload = 1,
            [Description("Пожарная опасность, ППП")]
            fire = 2,
            [Description("Ревизия")]
            inspection = 3,
            [Description("Управление из МП")]
            managmentMP = 4,
            [Description("МП1")]
            MP1 = 5,
            [Description("Ввод параметров")]
            parameterInput = 6,
            [Description("МП2")]
            MP2 = 7,
            [Description("Корректировочный рейс")]
            correct = 8,
            [Description("Утренний режим")]
            morning = 9,
            [Description("Вечерний режим")]
            evening = 10,
            [Description("С проводником")]
            attendant = 11,
            [Description("Дистанционное отключение")]
            liftOff = 12,
            [Description("Режим авария")]
            accidentMode = 13,
            [Description("Сейсмическая опасность")]
            earthQuake = 14,
            [Description("Больничный режим")]
            hospital = 15,
            [Description("Аварийная остановка")]
            fault = 16,
            [Description("Режим Out of Service")]
            outOfService = 17,
            [Description("Режим пожарной тревоги")]
            fire1 = 18,
            [Description("Режим ППП")]
            fire2 = 19,
            [Description("Режим эвакуации")]
            evacuation = 20,
            [Description("Режим VIP")]
            vip = 21,
            [Description("Независимый режим работы")]
            independed = 22,
            [Description("Режим парковки")]
            parking = 23,
            [Description("Режим приоритет вызовов")]
            LandingPriority = 24,
            [Description("Режим приоритет приказов")]
            CarPriority = 25,
            [Description("Эскалатор остановлен по STOP")]
            stopEscalator = 26,
        }

        public enum Doors
        {
            [Description("Состояние дверей не определено")]
            undefined = 0,
            [Description("Двери открываются")]
            open = 1,
            [Description("Двери полностью открыты")]
            all_open = 2,
            [Description("Двери закрываются")]
            close = 3,
            [Description("Двери полностью закрыты")]
            all_close = 4,
            [Description("Двери недозакрыты (приоткрыты)")]
            underclosed = 5,
            [Description("Двери заблокированы")]
            doors_lock = 254,
            [Description("Двери отсутствуют. Где двери?")]
            absence = 255,
        }

        public enum StageNum
        {
            [Description("Эскалатор")]
            escalator = 255,
            [Description("Неизвестно")]
            stage0 = 0,
            [Description("1 этаж")]
            stage1 = 1,
            [Description("2 этаж")]
            stage2 = 2,
            [Description("3 этаж")]
            stage3 = 3,
            [Description("4 этаж")]
            stage4 = 4,
            [Description("5 этаж")]
            stage5 = 5,
            [Description("6 этаж")]
            stage6 = 6,
            [Description("7 этаж")]
            stage7 = 7,
            [Description("8 этаж")]
            stage8 = 8,
            [Description("9 этаж")]
            stage9 = 9,
            [Description("10 этаж")]
            stage10 = 10,
            [Description("11 этаж")]
            stage11 = 11,
            [Description("12 этаж")]
            stage12 = 12,
            [Description("13 этаж")]
            stage13 = 13,
            [Description("14 этаж")]
            stage14 = 14,
            [Description("15 этаж")]
            stage15 = 15,
            [Description("16 этаж")]
            stage16 = 16,
            [Description("17 этаж")]
            stage17 = 17,
            [Description("18 этаж")]
            stage18 = 18,
            [Description("19 этаж")]
            stage19 = 19,
            [Description("20 этаж")]
            stage20 = 20,
            [Description("21 этаж")]
            stage21 = 21,
            [Description("22 этаж")]
            stage22 = 22,
            [Description("23 этаж")]
            stage23 = 23,
            [Description("24 этаж")]
            stage24 = 24,
            [Description("25 этаж")]
            stage25 = 25,
            [Description("26 этаж")]
            stage26 = 26,
            [Description("27 этаж")]
            stage27 = 27,
            [Description("28 этаж")]
            stage28 = 28,
            [Description("29 этаж")]
            stage29 = 29,
            [Description("30 этаж")]
            stage30 = 30,
            [Description("31 этаж")]
            stage31 = 31,
            [Description("32 этаж")]
            stage32 = 32,
            [Description("33 этаж")]
            stage33 = 33,
            [Description("34 этаж")]
            stage34 = 34,
            [Description("35 этаж")]
            stage35 = 35,
            [Description("36 этаж")]
            stage36 = 36,
            [Description("37 этаж")]
            stage37 = 37,
            [Description("38 этаж")]
            stage38 = 38,
            [Description("39 этаж")]
            stage39 = 39,
            [Description("40 этаж")]
            stage40 = 40,
            [Description("41 этаж")]
            stage41 = 41,
            [Description("42 этаж")]
            stage42 = 42,
            [Description("43 этаж")]
            stage43 = 43,
            [Description("44 этаж")]
            stage44 = 44,
            [Description("45 этаж")]
            stage45 = 45,
            [Description("46 этаж")]
            stage46 = 46,
            [Description("47 этаж")]
            stage47 = 47,
            [Description("48 этаж")]
            stage48 = 48,
            [Description("49 этаж")]
            stage49 = 49,
            [Description("50 этаж")]
            stage50 = 50,
            [Description("51 этаж")]
            stage51 = 51,
            [Description("52 этаж")]
            stage52 = 52,
            [Description("53 этаж")]
            stage53 = 53,
            [Description("54 этаж")]
            stage54 = 54,
            [Description("55 этаж")]
            stage55 = 55,
            [Description("56 этаж")]
            stage56 = 56,
            [Description("57 этаж")]
            stage57 = 57,
            [Description("58 этаж")]
            stage58 = 58,
            [Description("59 этаж")]
            stage59 = 59,
            [Description("60 этаж")]
            stage60 = 60,
            [Description("61 этаж")]
            stage61 = 61,
            [Description("62 этаж")]
            stage62 = 62,
            [Description("63 этаж")]
            stage63 = 63,
            [Description("64 этаж")]
            stage64 = 64,
        }

        [Flags]
        public enum LiftStatus : ulong
        {
            [Description("Вставлен сервисный ключ (Режим ТО)")]
            key = 0x00000001,
            [Description("Был рестарт блока")]
            blockrestart = 0x00000002,
            [Description("Многократный реверс дверей")]
            reversedoors = 0x00000004,
            [Description("Проникновение в МП")]
            prenetrationMP = 0x00000008,
            [Description("Отсутствует напряжение в цепи управления")]
            notension = 0x00000010,
            [Description("Зажата кнопка СТОП в кабине лифта")]
            stopbuttton = 0x00000020,
            [Description("Не закрыта дверь кабины")]
            doorcabin = 0x00000040,
            [Description("Разрыв цепи безопасности")]
            PCbreak = 0x00000080,
            [Description("Не сработал датчик УБ")]
            UBsensor = 0x00000100,
            [Description("КЗ цепи безопасности")]
            KZPC = 0x00000200,
            [Description("Вызов диспетчера")]
            calldispather = 0x00000400,
            [Description("Несанкционированное движение кабины")]
            unauthorized = 0x00000800,
            [Description("Авария главного привода по УКСЛ")]
            UKSL = 0x00001000,
            [Description("Авария привода дверей")]
            doordrive = 0x00002000,
            [Description("Проникновение в шахту")]
            mine = 0x00004000,
            [Description("Неисправность УБ")]
            UB = 0x00008000,
            [Description("Аварийная блокировка лифта")]
            liftlock = 0x00010000,
            [Description("Открыто МП")]
            openMP = 0x00020000,
            [Description("Кабина стоит между этажами")]
            betweenfloors = 0x00040000,
            [Description("Не сработал датчик ДК")]
            DK = 0x00080000,
            [Description("Нет связи со станцией управления")]
            noconnect = 0x00100000,
            [Description("Главный привод включен")]
            maindriveon = 0x00200000,
            [Description("Бит пользователя")]
            bituser = 0x00400000,
            [Description("Перемычка пускателя")]
            starter = 0x00800000,
            [Description("Блокировка РД без РКД")]
            RDnoRKD = 0x01000000,
            [Description("Вызов диспетчера из МП")]
            calldispatcherfrom = 0x02000000,
            [Description("Неисправность тракта ГГС кабины лифта")]
            GGScabin = 0x04000000,
            [Description("Состояние USER1")]
            USER1 = 0x0100000000,
            [Description("Состояние USER2")]
            USER2 = 0x0200000000,
            [Description("Состояние USER3")]
            USER3 = 0x0400000000,
            [Description("Состояние USER4")]
            USER4 = 0x0800000000,
            [Description("Резервное питание ЛБ")]
            backupLB = 0x1000000000,
            [Description("Переменное напряжение в ЦБ")]
            ACvoltage = 0x2000000000,
            [Description("Пожарная опасность")]
            FIRE = 0x4000000000,
            [Description("Неисправна батарея питания")]
            battaryerror = 0x8000000000,
        }
    }
}
