﻿using UnityEngine;
using UnityEngine.AI;

public static class Nick
{
    public static string[] names = new string[] {"Limbo",
"Носок судьбы",
"kosmos",
"huligan",
"FOXY",
"Demon",
"HowlClaw",
"СвяТой_ТапоК",
"DarK_Knigt",
"Torchok",
"Joker",
"NeZoX",
"Я ТуТ_Ты_ТрУп0_0",
"Белый кот",
"Страх",
"_Мария Ивановна_",
"ОпАSнЫй ВоЗрАSт",
"K I N G",
"Limbo",
"Cr1stal",
"Lemon4ik",
"GHOST",
"BlanderBot",
"3Jlou_4uTep",
"V1RUS",
"КрАсАвчИк",
"FINISH",
"Утка_в_тапках",
"СтРеЛоК",
"Fluffy",
"Святой",
"|Power| 4ekHyTblu",
"GaMEr",
"He}I{g@H4uk",
"Mr. Zadrot",
"Haker",
"W1zarD",
"БорЗая",
"_LegenDa_",
"JoKeR :D",
"Злобный_бульбулятор",
"Sa[Y]reX",
"Шаман-нарк",
"AnGeL",
"Agressor",
"KiSS_Ka",
"No_i",
"KoTuk",
"D_O_M_I_N_A_T_O_R",
"CJIoHuK",
"IceStorm",
"KukU EpTA",
"НеНавиСть",
"DoNaT1k",
"Высоковольтный Майонез",
"LIJyXeP",
"Доктор Вазелин",
"Kak_TaK?",
"N.E.O.N",
"Девчонка в наушниках",
"Vitaminka",
"Red_Wolf_777",
"H@rk0Tik",
"В_КеДАхХх",
"CmeTanKa",
"Mirrox",
"Aim41K",
"ShOoTeR",
"MaJIeHkuu_Ho_BeJIuKuu",
"Zigzag",
"3aHo3a",
"LiMon4k",
"Ceme4ka",
"SmoKKeR",
"Lиsичка",
"Летучий Олень",
"Nessa",
"N.E.V.E.N",
"G.R.I.Z.Z.L.I",
"AliceFox",
"I Cmpaх",
"ZikZak",
"4иRКаШ",
"АНТИ ПЕТУХ",
"DIVERSANT_95",
"Mr.Negotive",
"-_-RuBiX-_-",
"Beep Beep I’am Jeep",
"JIuMoH4eG",
"Enigma",
"-=HarDcore=-",
"XuMuK",
"3JIou_4uTeP",
"I am LEGENDA",
"K_I_N_G",
"pr!zrak?!",
"Сын_Маминой_Подруги",
"Cherry",
"Mr.GameFun",
"l7ocJleDHuu_CaMyPau",
"MANDARIN",
"^|^NEOбычный(~)GUсь",
"FeNiKs",
"Foxy_[P]i]r]a]t]e]",
"М о р я к",
"Эрагон",
"SkILLzAr",
"RUDI",
"Следующая остановка – сердце",
"Black_Wolf",
"GaRDeX",
"Magic",
"Good_Joker",
"KyKyPy3a",
"Это_Недоразумение",
"4eJIoBeK",
"Чучундра",
"-k0FFe-",
"IIo3uTuB_Ha_M|/|H|/|MyM",
"Revers",
"Ромашка_с_ядом",
"Суслик_переросток",
"Alpha",
"FeLix",
"Kacnuuckuu_moHcTp",
"ZeFir",
"G_L_O_B_A_L",
"STRANIK",
"FoXy",
"Y2J",
"Queenie",
"Cherry_Pie",
"DEADSHOT",
"Сherry",
"Hanter",
"SIREX",
"Fantasy",
"Дедушка с веслом",
"Бесшмертная_Бестия",
"Bambi",
"MamBa",
"MiNi_ZLoB@",
"Slasher",
"IIyJLu_oT_6a6yJLu",
"Карманный Блоходав",
"MaxTheGamer",
"ПА|-|ДоРа",
"B_R_O_L_Y_K",
"DELIRIOUS►PLAY",
"Vito_Scaletta",
"DoRoGa_SmErTi",
"mQ",
"GADZILO",
"СТАРЫЙ ДРУГ",
"4eByRaShKa",
"kefir",
"JayS",
"mamka adminA",
"NedStreX",
"T@NK pr!zrak",
"НУБОкиплер",
"Ezios",
"Mr. Zamo",
"Sweetheart",
"VeroN",
"Канеки кен",
"Fire fly",
"ArTeMk@",
"HELL  BOY",
"Палач",
"Bubbly",
"DEAD-_-Е_Y_E",
"Doc",
"I am king you noob",
"TauRus",
"Argon",
"CyxarЬIk",
"NyanCat",
"G[A]Me[R]",
"G_R_1_Z_Z",
"MaJIeHKuu’ KaIIuTa[H]",
"FrizYT",
"RedHulk",
"assassin",
"Чучело-Мяучело",
"Шалтай",
"ШИПОКРЫЛ",
"Bella",
"Jopa",
"Klaimmor",
"Fazer",
"Frost1k",
"LeMoN",
"Я_вернулась",
"[MoU5e]JxBadMood",
"FireBOY",
"Forever",
"Kitty",
"Lixo",
"Tekilla",
"WildHare",
"Jasper",
"Teнь",
"Нарколог",
":AnGeL:",
"Doctor",
"KoTuk 400kg",
"NoName",
"Акива",
"Cherry Lips",
"rpu3Jlu",
"Skrill",
"МOIIIK@",
"4EVERALON3",
"Barmaley",
"Bаunty",
"Coco",
"EzzzBOX",
"N0len",
"SkyDeaD",
"Dark_Reaper",
"E2E4",
"GhOsTiK",
"Kuki",
"Агент007",
"Dante",
"InKey",
"ISTRIBITEL",
"KILL_StAlKeR",
"M0nika",
"MeLisa",
"Piffy",
"SkOrPiOnUs",
"Амон_ра",
"dazz",
"xFaZe",
"ZABY",
"Darz",
"HITM@N",
"LAS|SA",
"Link",
"Liona",
"Morkov4ik",
"Mr.Agressor",
"Безумный_автобус",
"ВейПеР",
"Лапочка",
"DeMpDeeZ",
"FlooMeer",
"GameMen",
"GONZO",
"Kuku",
"L_A_R_o_N",
"Miss Priss",
"MonAmourKa",
"NAFYL",
"Nettron",
"NightKi11er",
"RaVeN",
"zEE",
"Загорелая роза",
"Слеш",
"BaRsIk",
"Gabby",
"He4ayHo",
"Iguana",
"Klaus",
"Muhammad-ali",
"Sting",
"Миллди",
"chpon’k po golowam",
"Dustup",
"EnderDe",
"Essence",
"HouruSinkara",
"Kisa",
"LeamuR",
"MoDiE",
"speed",
"SpeedBeast",
"Ама3онка",
"Каби",
"Aie",
"Ballistic",
"Beautiful",
"Black Look",
"BORODOTA",
"Cherro",
"Chilly",
"Dark wolf",
"Enroxes",
"Howfaralice",
"KiberTron",
"Mist(er)ror",
"Mo1oko",
"P@werate",
"pRO_Game",
"Одинокийй волкк",
"Artcross",
"AsmodeuZ",
"Bambina",
"ElectroInUp",
"Вова",
"Дима",
"Леша",
"Барзан",
"Паша",
"Матвей",
"Батраков",
"Илья" };
}

public class EnemyController : MonoBehaviour
{
    private Transform _whereToGo;
    private NavMeshAgent _agent;
    private CharacterController _charCont;
    [SerializeField] private EnemySkillGroup _esg;

    public void SetLVL(int lvl)
    {
        for (int i = 0; i < lvl; ++i)
        {
            _esg.SelectSkill(_esg.RandomSkill());
        }
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _charCont = GetComponent<CharacterController>();
        _charCont._speed = 0;
        _charCont.onDeath += Death;
        _charCont.onLVLUp += LvlUp;
        _charCont.SetNick(Nick.names[Random.Range(0,Nick.names.Length)]);
        FindWhereToGo();
    }

    private void Update()
    {
        _charCont.Move(_agent.desiredVelocity.normalized);
        if (_charCont._target != null)
            _charCont.Shoot();
        if (Vector3.Distance(_agent.nextPosition, transform.position) < 1f || _agent.nextPosition == null)
            FindWhereToGo();
    }

    private void FindWhereToGo()
    {
        _whereToGo = FindAnyObjectByType<Diamond>().transform;
        _agent.SetDestination(_whereToGo.position);
    }

    private void Death()
    {
        _agent.isStopped = true;
        _agent.enabled = false;
        enabled = false;
    }

    public void LvlUp()
    {
        _esg.SelectSkill(_esg.RandomSkill());
    }
}
