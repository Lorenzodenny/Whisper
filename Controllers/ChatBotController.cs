using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Whisper.Controllers
{
    public class ChatBotController : Controller
    {
        private static readonly Random random = new Random();
        private static string lastResponse = null; 

        public ActionResult GetBotResponse(string Message)
        {
            string[] responses;

            // conversione in minuscolo del messaggio utente
            string message = Message.ToLower();

            // Uso di un array di parole chiave per identificare la categoria di risposta
            string[] successKeywords = new string[] { "successo", "riuscita", "vittoria", "conquista", "trionfo" };
            string[] happinessKeywords = new string[] { "felice", "felicità", "gioia", "contento", "beato" };
            string[] learningKeywords = new string[] { "imparare", "apprendimento", "studio", "educazione", "conoscenza" };
            string[] challengeKeywords = new string[] { "sfida", "difficoltà", "ostacolo", "prova", "compito" };
            string[] healthKeywords = new string[] { "salute", "benessere", "forma", "fitness", "sanità" };
            string[] botWellbeingKeywords = new string[] { "come stai", "come stai?", "tutto bene", "stai bene", "come va", "come va?", "va tutto bene" };
            string[] entertainmentKeywords = new string[] { "film", "serie", "divertimento", "tempo libero", "hobby" };
            string[] relationshipKeywords = new string[] { "amore", "amicizia", "relazione", "partner", "famiglia" };
            string[] angerKeywords = new string[] { "arrabbiato", "rabbia", "furia", "irritato", "infuriato" };
            string[] failureKeywords = new string[] { "fallimento", "fallito", "sconfitta", "non riesco", "incapace" };
            string[] exclusionKeywords = new string[] { "escluso", "esclusione", "ignorato", "solo", "isolato" };
            string[] workStressKeywords = new string[] { "lavoro stressante", "stress lavoro", "pressione lavoro", "sovraccarico lavoro" };
            string[] griefKeywords = new string[] { "lutto", "perdita", "morte", "dolore", "sofferenza" };
            string[] insecurityKeywords = new string[] { "insicuro", "insicurezza", "autostima bassa", "non mi sento abbastanza", "dubbi su di me" };
            string[] futureAnxietyKeywords = new string[] { "ansia futuro", "preoccupato per il futuro", "incerto sul futuro" };
            string[] lowMotivationKeywords = new string[] { "demotivato", "bassa motivazione", "non ho voglia", "non mi sento motivato" };
            string[] inadequacyKeywords = new string[] { "inadeguato", "non all'altezza", "non sono abbastanza", "mi sento inferiore" };
            string[] stressKeywords = new string[] { "stressato", "sovraccarico", "pressione", "stress" };
            string[] sadnessKeywords = new string[] { "triste", "depresso", "abbattuto", "infelice", "tristezza", "abbattuto", "giorni bui" };
            string[] lonelinessKeywords = new string[] { "solo", "solitudine", "isolato", "senza amici" };
            string[] frustrationKeywords = new string[] { "frustrato", "frustrazione", "irritato", "arrabbiato" };
            string[] performanceAnxietyKeywords = new string[] { "ansia da prestazione", "stress da lavoro", "performance anxiety", "paura di fallire", "nervosismo prima di", "paure pre-evento" };
            string[] changeManagementKeywords = new string[] { "cambiamento", "transizione", "nuovo inizio", "cambi di vita" };
            string[] boredomKeywords = new string[] { "noia", "annoiato", "niente da fare", "tempo libero" };
            string[] jealousyKeywords = new string[] { "geloso", "gelosia", "invidia", "sentirsi minacciato" };
            string[] criticismKeywords = new string[] { "criticato", "critica", "sentirsi giudicato", "feedback negativo" };
            string[] changeFearKeywords = new string[] { "paura del cambiamento", "cambiamento spaventa", "timore del nuovo", "incerto sul cambiamento" };
            string[] overloadKeywords = new string[] { "sovraccarico", "troppo da fare", "schiacciato", "responsabilità eccessive" };
            string[] trustIssuesKeywords = new string[] { "mancanza di fiducia", "poca fiducia", "insicurezza", "non mi fido" };
            string[] worthlessnessKeywords = new string[] { "inutile", "senso di inutilità", "non valgo", "non contribuisco" };
            string[] irritabilityKeywords = new string[] { "irritabile", "facilmente irritato", "irritazione", "nervosismo" };
            string[] judgmentFearKeywords = new string[] { "paura del giudizio", "timore di essere giudicato", "preoccupazione per l'opinione altrui", "insicurezza sociale" };
            string[] socialOverwhelmKeywords = new string[] { "troppi impegni", "sopraffatto dagli eventi", "calendario pieno", "stress da socializzazione" };
            string[] disappointmentKeywords = new string[] { "deluso", "delusione", "aspettative non soddisfatte", "speranze infrante" };
            string[] stuckKeywords = new string[] { "bloccato", "in stallo", "senza direzione", "non progredisco" };
            string[] uncertaintyKeywords = new string[] { "incerto", "futuro incerto", "non so cosa fare", "paura del futuro" };
            string[] decisionOverwhelmKeywords = new string[] { "decisioni difficili", "non so scegliere", "paralizzato dalle scelte", "incerto sulle decisioni" };
            string[] rejectionKeywords = new string[] { "rifiutato", "fallito", "bocciato", "non sono riuscito" };
            string[] selfCriticismKeywords = new string[] { "autocritico", "troppo critico con me stesso", "non sono mai soddisfatto", "mi critico sempre" };
            string[] socialPressureKeywords = new string[] { "pressione sociale", "aspettative degli altri", "conformità sociale" };
            string[] lossKeywords = new string[] { "perdita", "lutti", "addio", "cordoglio" };
            string[] curiosityKeywords = new string[] { "curioso", "interessato", "intrigato", "desideroso di sapere", "voglia di scoprire" };
            string[] exhaustionKeywords = new string[] { "esausto", "stanco morto", "senza energie", "sfinimento", "consumato" };
            string[] explorationKeywords = new string[] { "esplorare", "avventura", "scoprire", "viaggiare", "esplorazione" };
            string[] empathyKeywords = new string[] { "comprendere", "empatia", "solidarietà", "capire gli altri", "condivisione di sentimenti" };
            string[] serenityKeywords = new string[] { "serenità", "pace interiore", "calma", "tranquillità", "rilassato" };
            string[] excitementKeywords = new string[] { "eccitato", "entusiasta", "euforia", "emozionato", "fervore" };
            string[] gratitudeKeywords = new string[] { "grato", "grazie", "apprezzamento", "riconoscenza", "valutare" };
            string[] discontentKeywords = new string[] { "insoddisfatto", "scontento", "deluso", "mancanza", "insufficiente" };
            string[] nostalgiaKeywords = new string[] { "nostalgico", "rimpianto", "ricordi", "passato", "memorie" };
            string[] inspirationKeywords = new string[] { "ispirato", "motivato", "stimolato", "influenzato", "ispirazione" };
            string[] anxietyKeywords = new string[] { "ansioso", "preoccupato", "inquieto", "nervoso", "tensione" };
            string[] contentmentKeywords = new string[] { "contento", "soddisfatto", "appagato", "felice", "realizzato" };
            string[] reliefKeywords = new string[] { "sollevato", "liberato", "sollievo", "rilassamento", "liberazione" };
            string[] optimismKeywords = new string[] { "ottimista", "speranzoso", "positivo", "fiducioso", "incoraggiato" };
            string[] melancholyKeywords = new string[] { "malinconico", "melanconico", "tristezza lieve", "riflessivo", "nostalgico" };
            string[] vigorKeywords = new string[] { "energia", "vitalità", "vigoroso", "dinamico", "vivace" };
            string[] discomfortKeywords = new string[] { "scomodo", "disagio", "inquietudine", "malessere", "difficoltà" };
            string[] serenityyKeywords = new string[] { "sereno", "calmo", "pace", "tranquillo", "quieto" };
            string[] comfortKeywords = new string[] { "comfort", "conforto", "consolazione", "solace", "sollievo" };
            string[] excitementsKeywords = new string[] { "eccitazione", "thrill", "brivido", "emozione", "adrenalina" };
            string[] fatigueKeywords = new string[] { "stanchezza", "fatica", "affaticamento", "esaurimento", "lassitudine" };
            string[] noveltyKeywords = new string[] { "novità", "nuovo", "fresco", "originale", "inedito" };
            string[] serendipityKeywords = new string[] { "fortuna", "casualità", "destino", "serendipità", "colpo di fortuna" };
            string[] doubtKeywords = new string[] { "dubbio", "incertezza", "esitazione", "indecisione", "perplessità" };
            string[] calmnessKeywords = new string[] { "calma", "quiete", "serenità", "placido", "tranquillo" };
            string[] enthusiasmKeywords = new string[] { "entusiasmo", "passione", "zelo", "ardore", "vivacità" };
            string[] confusionKeywords = new string[] { "confuso", "disorientato", "incerto", "sconcertato", "perplesso" };
            string[] greetingKeywords = new string[] { "ciao", "salve", "buongiorno", "buonasera", "ehilà" };
            string[] farewellKeywords = new string[] { "arrivederci", "addio", "a presto", "buonanotte" };
            string[] thanksKeywords = new string[] { "grazie", "apprezzo", "ringrazio", "grato", "ti ringrazio" };
            string[] movieRecommendationKeywords = new string[] { "suggeriscimi un film", "consigliami un film", "film da vedere", "buon film da guardare", "film interessante" };
            string[] musicRecommendationKeywords = new string[] { "suggeriscimi della musica", "consigliami musica", "musica da ascoltare", "buona musica da ascoltare", "musica interessante" };
            string[] bookRecommendationKeywords = new string[] { "suggeriscimi un libro", "consigliami un libro", "libro da leggere", "buon libro da leggere", "libro interessante" };
            string[] personalGrowthKeywords = new string[] { "crescita personale", "auto-miglioramento", "sviluppo personale", "autorealizzazione", "migliorarsi" };
            string[] careerAdviceKeywords = new string[] { "consigli carriera", "orientamento professionale", "sviluppo carriera", "scelte professionali", "avanzamento carriera" };
            string[] healthWellnessKeywords = new string[] { "salute benessere", "consigli salute", "stare bene", "vivere sano", "benessere fisico" };
            string[] hobbiesInterestsKeywords = new string[] { "hobby", "interessi", "tempo libero", "passatempo", "attività preferite" };
            string[] personalRelationshipsKeywords = new string[] { "relazioni personali", "amicizie", "rapporti interpersonali", "relazioni familiari", "collegamenti umani" };
            string[] timeManagementKeywords = new string[] { "gestione del tempo", "organizzazione", "pianificazione", "efficienza", "ottimizzazione del tempo" };
            string[] travelPlanningKeywords = new string[] { "pianificazione viaggio", "viaggiare", "consigli viaggio", "destinazioni", "itinerario" };
            string[] learningOpportunitiesKeywords = new string[] { "opportunità di apprendimento", "corsi", "educazione", "formazione", "studiare" };
            string[] mindfulnessKeywords = new string[] { "consapevolezza", "mindfulness", "meditazione", "calma interiore", "rilassamento mentale" };
            string[] currentEventsKeywords = new string[] { "attualità", "notizie", "eventi recenti", "aggiornamenti mondiali", "ultime notizie" };
            string[] emotionalWellbeingKeywords = new string[] { "benessere emotivo", "salute mentale", "equilibrio emotivo", "gestione delle emozioni", "stabilità emotiva" };
            string[] financialManagementKeywords = new string[] { "gestione finanziaria", "finanze personali", "consigli finanziari", "budgeting", "investimenti" };
            string[] sustainabilityKeywords = new string[] { "sostenibilità", "ambiente", "ecologia", "risparmio energetico", "riciclo" };
            string[] technologyAdvancementKeywords = new string[] { "tecnologia", "innovazioni", "sviluppo tech", "gadget", "futuro della tecnologia" };
            string[] culinaryTravelKeywords = new string[] { "viaggi culinari", "cucina internazionale", "gastronomia", "sapori del mondo", "cibo e viaggi" };
            string[] emotionalSupportKeywords = new string[] { "ho bisogno di supporto", "mi sento giù", "sono triste", "ho bisogno di aiuto", "sono solo" };
            string[] encouragementKeywords = new string[] { "incoraggiamento", "motivami", "ho bisogno di coraggio", "aiutami a continuare", "dammi forza" };
            string[] personalReflectionKeywords = new string[] { "riflessione personale", "autoanalisi", "introspezione", "comprendere me stesso", "autoconsapevolezza" };
            string[] resilienceKeywords = new string[] { "resilienza", "superare ostacoli", "affrontare difficoltà", "forza interiore", "recupero dopo una caduta" };
            string[] selfRealizationKeywords = new string[] { "realizzazione personale", "scoprire se stessi", "raggiungere obiettivi", "soddisfazione personale", "capire i propri desideri" };
            string[] overcomingChallengesKeywords = new string[] { "superare sfide", "affrontare problemi", "superare barriere", "vincere difficoltà", "gestire ostacoli" };
            string[] selfCompassionKeywords = new string[] { "auto-compassione", "essere gentile con se stessi", "amore per sé", "autocura", "perdonarsi" };
            string[] stressManagementKeywords = new string[] { "gestione dello stress", "ridurre lo stress", "ansia", "rilassamento", "calma" };
            string[] spiritualGrowthKeywords = new string[] { "crescita spirituale", "sviluppo spirituale", "meditazione spirituale", "consapevolezza spirituale", "percorso spirituale" };
            string[] optimismoKeywords = new string[] { "ottimismo", "pensiero positivo", "vedere il lato positivo", "speranza", "positività" };
            string[] forgivenessKeywords = new string[] { "perdono", "perdonare", "lasciar andare", "superare il rancore", "reconciliazione" };
            string[] adaptingChangeKeywords = new string[] { "adattarsi al cambiamento", "gestire il cambiamento", "flessibilità", "accettare il cambiamento", "resilienza al cambiamento" };
            string[] relationshipManagementKeywords = new string[] { "gestione delle relazioni", "problematiche relazionali", "migliorare le relazioni", "consigli relazionali", "relazioni sane" };
            string[] selfEsteemKeywords = new string[] { "autostima", "fiducia in sé", "valore personale", "amor proprio", "sentirsi valorizzato" };
            string[] overcomingLonelinessKeywords = new string[] { "superare la solitudine", "sentirsi soli", "connessioni sociali", "fare amicizia", "combattere la solitudine" };
            string[] angerManagementKeywords = new string[] { "gestione della rabbia", "controllare la rabbia", "calmare la rabbia", "rabbia", "furioso" };
            string[] seekingInspirationKeywords = new string[] { "cercare ispirazione", "bisogno di ispirazione", "motivazione", "stimoli creativi", "ispirarmi" };
            string[] improvingCommunicationKeywords = new string[] { "migliorare la comunicazione", "comunicare meglio", "abilità comunicative", "dialogo efficace", "parlare chiaramente" };
            string[] selfCriticismManagementKeywords = new string[] { "autocritica", "essere troppo critici con se stessi", "superare l'autocritica", "criticarsi meno", "critica interiore" };
            string[] emotionalResilienceKeywords = new string[] { "resilienza emotiva", "forza interiore", "gestire le emozioni", "stabilità emotiva", "resistenza emotiva" };
            string[] dailyWellbeingKeywords = new string[] { "benessere quotidiano", "vivere bene ogni giorno", "routine salutare", "stare bene", "quotidianità felice" };
            string[] talkKeywords = new string[] { "parlare", "discutere", "conversare", "comunicare", "esprimersi" };
            string[] chatKeywords = new string[] { "chiacchierare", "battere", "gossip", "conversazione leggera", "dialogare" };
            string[] conversationKeywords = new string[] { "avere una conversazione", "dialogare", "interloquire", "scambiare idee", "discorrere" };
            string[] exploreKeywords = new string[] { "esplorare", "scoprire", "approfondire", "investigare", "esaminare", "analizzare", "sondare" };
            string[] timeManagementsKeywords = new string[] { "gestire il tempo", "pianificazione", "organizzazione", "programmazione", "allocazione del tempo", "ottimizzazione del tempo", "scheduling" };
            string[] stressReliefKeywords = new string[] { "alleviare lo stress", "riduzione dello stress", "rilassamento", "distensione", "calmare", "rasserenare", "decompressione" };
            string[] selfReflectionKeywords = new string[] { "riflessione", "autoanalisi", "introspezione", "autovalutazione", "consapevolezza di sé" };
            string[] moodManagementKeywords = new string[] { "gestione dell'umore", "regolazione emotiva", "controllo delle emozioni", "stabilizzazione dell'umore", "equilibrio emotivo" };
            string[] copingStrategiesKeywords = new string[] { "strategie di coping", "metodi di adattamento", "tecniche di gestione dello stress", "resilienza", "meccanismi di adattamento" };
            string[] emotionalInsightKeywords = new string[] { "comprensione emotiva", "intuizione emotiva", "consapevolezza dei sentimenti", "percezione delle emozioni", "sensibilità emotiva" };
            string[] personalsGrowthKeywords = new string[] { "sviluppo personale", "crescita personale", "autorealizzazione", "evoluzione personale", "miglioramento personale" };
            string[] relationshipInsightsKeywords = new string[] { "intuizioni relazionali", "dinamiche relazionali", "comprensione interpersonale", "relazioni interpersonali", "comprensione delle relazioni" };
            string[] selfAwarenessKeywords = new string[] { "autoconsapevolezza", "introspezione", "riflessione su sé stessi", "conoscenza interiore", "comprensione di sé" };
            string[] overcomingFearKeywords = new string[] { "superare la paura", "affrontare le paure", "vincere la paura", "coraggio di fronte alla paura", "gestire l'ansia" };
            string[] emotionalExpressionKeywords = new string[] { "espressione emotiva", "condividere emozioni", "esprimere sentimenti", "comunicazione delle emozioni", "manifestare sentimenti" };
            string[] selfHealingKeywords = new string[] { "autoguarigione", "auto-cura", "recupero personale", "guarigione interiore", "benessere personale" };
            string[] emotionalManagementKeywords = new string[] { "gestione delle emozioni", "regolazione emotiva", "controllo delle emozioni", "bilanciamento emotivo", "salute emotiva" };
            string[] selfAcceptanceKeywords = new string[] { "autoaccettazione", "accettazione di sé", "amor proprio", "autostima", "auto-valutazione" };
            string[] innerPeaceKeywords = new string[] { "pace interiore", "tranquillità interna", "calma interna", "equilibrio interno", "serenità" };
            string[] emotionalHealingKeywords = new string[] { "guarigione emotiva", "recupero emotivo", "cura delle emozioni", "salute emotiva", "benessere emotivo" };
            string[] managingAnxietyKeywords = new string[] { "gestire l'ansia", "ansia", "nervosismo", "preoccupazione", "tensione" };
            string[] buildingResilienceKeywords = new string[] { "costruire resilienza", "resilienza", "forza interiore", "recupero", "adattabilità" };
            string[] improvingSelfWorthKeywords = new string[] { "migliorare il proprio valore", "autostima", "valore personale", "amor proprio", "sentirsi valorizzati" };
            string[] findingPurposeKeywords = new string[] { "trovare uno scopo", "scopo di vita", "senso della vita", "missione personale", "obiettivo di vita" };
            string[] emotionalsHealingKeywords = new string[] { "guarigione emotiva", "recupero emotivo", "salute emotiva", "superare il dolore", "curare le ferite emotive" };
            string[] copingWithChangeKeywords = new string[] { "affrontare il cambiamento", "adattarsi al nuovo", "gestire il cambiamento", "accettare il cambiamento", "trasformazione personale" };
            string[] selfDiscoveryKeywords = new string[] { "scoperta di sé", "conoscenza di sé", "esplorazione personale", "capire se stessi", "auto-scoperta" };
            string[] lifeTransitionKeywords = new string[] { "transizione di vita", "nuovi inizi", "cambiamenti della vita", "nuove fasi", "evoluzione personale" };
            string[] personalBalanceKeywords = new string[] { "equilibrio personale", "bilanciamento vita-lavoro", "armonia interiore", "stare in equilibrio", "equilibrio vita quotidiana" };
            string[] familyConflictKeywords = new string[] { "conflitto familiare", "problemi in famiglia", "litigi con i parenti", "disaccordi familiari", "incomprensioni in famiglia" };
            string[] careerProgressionKeywords = new string[] { "avanzamento di carriera", "promozione", "sviluppo professionale", "crescita lavorativa", "progresso nel lavoro" };
            string[] decisionMakingKeywords = new string[] { "prendere decisioni", "scelta difficile", "indecisione", "esitazione", "confusione su cosa fare" };
            string[] emotionalRecoveryKeywords = new string[] { "recupero emotivo", "riprendersi emotivamente", "superare il dolore", "guarigione dopo un trauma", "ritrovare la serenità" };
            string[] boundarySettingKeywords = new string[] { "impostare limiti", "confini personali", "proteggere lo spazio personale", "limiti sani", "gestione delle relazioni" };
            string[] selfExpressionKeywords = new string[] { "espressione di sé", "mostrare autenticità", "essere se stessi", "condividere i propri pensieri", "esprimere le proprie emozioni" };
            string[] personalAcceptanceKeywords = new string[] { "accettazione personale", "autoaccettazione", "accettarsi", "amare se stessi", "accogliere i propri difetti" };
            string[] futurePlanningKeywords = new string[] { "pianificazione futura", "progetti futuri", "preparazione al futuro", "obiettivi a lungo termine", "sognare in grande" };
            string[] mentalClarityKeywords = new string[] { "chiarezza mentale", "pensiero chiaro", "liberarsi dalla confusione", "focalizzazione", "mente lucida" };
            string[] selfyDiscoveryKeywords = new string[] { "scoperta di sé", "autoconoscenza", "esplorazione personale", "capire se stessi", "viaggio interiore", "introspezione", "analisi personale" };
            string[] lifeBalanceKeywords = new string[] { "equilibrio vita", "bilanciamento lavoro-vita", "armonia personale", "stabilità quotidiana", "equilibrio tra lavoro e piacere", "gestione del tempo personale", "vita equilibrata" };
            string[] emotionalWisdomKeywords = new string[] { "saggezza emotiva", "intelligenza emotiva", "comprensione delle emozioni", "gestione dei sentimenti", "ascolto emotivo", "elaborazione emotiva", "maturità emotiva" };
            string[] needToTalkKeywords = new string[] { "voglio parlare", "ho bisogno di parlare", "bisogno di un ascolto", "conversazione necessaria", "sentirmi ascoltato" };
            string[] expressingNeedsKeywords = new string[] { "esprimere bisogni", "comunicare esigenze", "manifestare necessità", "chiarire desideri", "condividere richieste" };
            string[] seekingSupportKeywords = new string[] { "cerco supporto", "ho bisogno di sostegno", "voglio condividere", "mi sento solo", "ho bisogno di un consiglio" };
            string[] emotionaslSupportKeywords = new string[] { "ho bisogno di supporto emotivo", "mi sento giù", "ho bisogno di conforto", "sono triste", "mi sento solo" };
            string[] comfortingWordsKeywords = new string[] { "parole di conforto", "frasi rassicuranti", "messaggi di incoraggiamento", "sostegno morale", "supporto positivo" };
            string[] empathyExpressionsKeywords = new string[] { "mostrare empatia", "comprendere sentimenti", "essere solidali", "condividere emozioni", "esprimere affetto" };
            string[] expressionKeywords = new string[] { "espressione emotiva", "condividere emozioni", "esprimere sentimenti", "comunicazione delle emozioni", "manifestare sentimenti" };
            string[] introspectionKeywords = new string[] { "introspezione", "autoanalisi", "riflessione su sé stessi", "autovalutazione", "consapevolezza di sé" };
            string[] relaxationKeywords = new string[] { "rilassamento", "calma", "tranquillità", "pausa", "respiro profondo" };
            string[] motivationKeywords = new string[] { "motivazione", "ispirazione", "determinazione", "obiettivi", "realizzazione" };
            string[] selfCareKeywords = new string[] { "cura di sé", "benessere", "autostima", "riposo", "alimentazione sana" };
            string[] problemSolvingKeywords = new string[] { "risolvere problemi", "trovare soluzioni", "affrontare sfide", "pensiero critico", "decisioni" };
            string[] reflectionKeywords = new string[] { "riflessione", "introspezione", "meditazione", "pensieri profondi", "autoconsapevolezza" };
            string[] selfImprovementKeywords = new string[] { "miglioramento personale", "crescita personale", "sviluppo personale", "auto-miglioramento", "obiettivi di vita" };
            string[] positivityKeywords = new string[] { "ottimismo", "positività", "pensiero positivo", "gratitudine", "gioia di vivere" };
            string[] friendshipKeywords = new string[] { "amicizia", "compagnia", "solidarietà", "condivisione", "affetto sincero" };
            string[] creativityKeywords = new string[] { "creatività", "ispirazione", "espressione artistica", "pensiero laterale", "innovazione" };
            string[] communicationSkillsKeywords = new string[] { "abilità di comunicazione", "comunicazione efficace", "ascolto attivo", "empatia", "assertività" };
            string[] careerDevelopmentKeywords = new string[] { "sviluppo di carriera", "miglioramento professionale", "obiettivi di carriera", "crescita professionale", "successo lavorativo" };
            string[] physicalWellnessKeywords = new string[] { "benessere fisico", "salute", "fitness", "alimentazione sana", "attività fisica regolare" };
            string[] mentalHealthKeywords = new string[] { "salute mentale", "benessere psicologico", "equilibrio emotivo", "stress psicologico", "cura della mente" };
            string[] socialConnectionKeywords = new string[] { "connessioni sociali", "interazioni sociali", "relazioni significative", "comunità", "senso di appartenenza" };
            string[] goalSettingKeywords = new string[] { "impostazione degli obiettivi", "obiettivi personali", "obiettivi professionali", "pianificazione degli obiettivi", "realizzazione degli obiettivi" };
            string[] leadershipSkillsKeywords = new string[] { "abilità di leadership", "gestione delle persone", "motivazione dei team", "ispirare gli altri", "influenza positiva" };
            string[] conflictResolutionKeywords = new string[] { "risoluzione dei conflitti", "gestione dei conflitti", "negoziazione", "ascolto attivo", "mediatore" };
            string[] emotionalIntelligenceKeywords = new string[] { "intelligenza emotiva", "autoconsapevolezza", "regolazione emotiva", "empatia", "gestione delle relazioni" };
            string[] relationshipAdviceKeywords = new string[] { "consigli sulle relazioni", "amore", "amicizia", "famiglia", "comunicazione" };
            string[] healthyLivingKeywords = new string[] { "vita sana", "alimentazione", "fitness", "benessere fisico", "salute mentale" };
            string[] learningNewSkillsKeywords = new string[] { "apprendimento di nuove competenze", "crescita personale", "formazione", "auto-miglioramento", "sviluppo individuale" };
            string[] travelTipsKeywords = new string[] { "consigli per viaggiare", "turismo", "avventure", "esplorazione", "viaggiare in modo intelligente" };
            string[] hobbyIdeasKeywords = new string[] { "idee per hobby", "passioni", "interessi", "attività ricreative", "svago creativo" };
            string[] digitalWellnessKeywords = new string[] { "benessere digitale", "uso consapevole della tecnologia", "disconnessione digitale", "tempo offline", "gestione del tempo online" };
            string[] selfsCompassionKeywords = new string[] { "auto-compassione", "essere gentile con se stessi", "amore per sé", "autocura", "perdonarsi", "trattarsi bene", "autostima positiva", "gentilezza interna", "accettazione di sé", "cura personale" };
            string[] overcomingProcrastinationKeywords = new string[] { "procrastinazione", "rimandare", "dilazione", "evitare compiti", "posticipare", "gestione del tempo", "efficienza", "pigrizia", "mancanza di motivazione", "fatica a iniziare" };
            string[] enhancingEmotionalIntelligenceKeywords = new string[] { "intelligenza emotiva", "gestione delle emozioni", "comprensione emotiva", "ascolto attivo", "empatia", "autoregolazione", "autoconsapevolezza", "relazioni interpersonali", "risoluzione dei conflitti", "comunicazione emotiva" };
            string[] mindfulnessPracticesKeywords = new string[] { "pratiche di mindfulness", "meditazione mindfulness", "consapevolezza mentale", "attenzione plena", "esercizi di respirazione", "relax mentale", "pratica meditativa", "calma interna", "awareness", "presenza mentale" };
            string[] emotionalReleaseKeywords = new string[] { "rilascio emotivo", "esprimere emozioni", "liberazione sentimenti", "pianto terapeutico", "sfogarsi", "parlare delle emozioni", "condivisione del dolore", "accettazione delle emozioni", "espressione dei sentimenti", "catarsi emotiva" };
            string[] careerFulfillmentKeywords = new string[] { "realizzazione professionale", "soddisfazione lavorativa", "successo in carriera", "obiettivi professionali", "ambizione lavorativa", "crescita in lavoro", "pienezza lavorativa", "motivazione professionale", "aspirazioni di carriera", "soddisfazione del lavoro" };
            string[] ymindfulnessPracticesKeywords = new string[] { "pratiche di mindfulness", "meditazione mindfulness", "consapevolezza mentale", "attenzione plena", "esercizi di respirazione", "relax mentale", "pratica meditativa", "calma interna", "awareness", "presenza mentale" };
            string[] yemotionalReleaseKeywords = new string[] { "rilascio emotivo", "esprimere emozioni", "liberazione sentimenti", "pianto terapeutico", "sfogarsi", "parlare delle emozioni", "condivisione del dolore", "accettazione delle emozioni", "espressione dei sentimenti", "catarsi emotiva" };
            string[] ycareerFulfillmentKeywords = new string[] { "realizzazione professionale", "soddisfazione lavorativa", "successo in carriera", "obiettivi professionali", "ambizione lavorativa", "crescita in lavoro", "pienezza lavorativa", "motivazione professionale", "aspirazioni di carriera", "soddisfazione del lavoro" };
            string[] casualFarewellsKeywords = new string[] { "arrivederci", "addio", "a presto", "buonanotte" };
            string[] generalQuestionsKeywords = new string[] { "che ore sono", "qual è il meteo", "hai suggerimenti", "cosa mi consigli", "aiutami a scegliere" };
            string[] positiveExpressionsKeywords = new string[] { "grazie", "apprezzo molto", "sei stato utile", "sei fantastico", "buon lavoro" };
            string[] elizaInquiryKeywords = new string[] { "chi è eliza", "cos'è eliza", "parlami di eliza", "eliza", "storia di eliza", "perché ti chiami eliza" };
            string[] smallTalkKeywords = new string[] { "che si dice", "novità", "cosa mi racconti", "come va la vita", "tutto a posto" };
            string[] complimentsKeywords = new string[] { "sei un grande", "bravo", "ottimo lavoro", "sei il migliore", "ben fatto" };
            string[] casualRequestsKeywords = new string[] { "dammi una mano", "puoi aiutarmi", "ho bisogno di un consiglio", "che ne pensi", "mi dai un suggerimento" };
            string[] expressionsOfReliefKeywords = new string[] { "meno male", "sospiro di sollievo", "finalmente", "tutto bene che finisce bene", "si è risolto tutto" };
            string[] expressionsOfConfusionKeywords = new string[] { "non capisco", "sono confuso", "che confusione", "non mi è chiaro", "potresti spiegare meglio" };
            string[] expressionsOfUrgencyKeywords = new string[] { "è urgente", "fammi sapere subito", "non c'è tempo", "affrettati", "bisogno immediato" };
            string[] expressionsOfHappinessKeywords = new string[] { "sono felice", "che bello", "fantastico", "troppo bello", "sono contento", "mi fa piacere", "mi rende felice" };
            string[] expressionsOfDisappointmentKeywords = new string[] { "che peccato", "sono deluso", "quanto mi dispiace", "non è andata bene", "speravo meglio", "è un disastro", "che delusione" };
            string[] expressionsOfEncouragementKeywords = new string[] { "coraggio", "non mollare", "tieni duro", "puoi farcela", "forza", "sei capace", "non arrenderti" };
            string[] expressionsOfGratitudeKeywords = new string[] { "grazie mille", "molto obbligato", "ti ringrazio", "apprezzo molto", "sei un tesoro", "grazie di cuore", "sei gentilissimo" };
            string[] expressionsOfCuriosityKeywords = new string[] { "mi chiedo", "sono curioso", "come mai", "perché", "puoi spiegare", "dimmi di più", "ho una domanda" };
            string[] expressionsOfSkepticismKeywords = new string[] { "sei sicuro", "non ne sono convinto", "davvero", "può essere", "mi sembra strano", "non ci credo", "sembra improbabile" };
            string[] expressionsOfJoyKeywords = new string[] { "sono euforico", "che felicità", "sto benissimo", "giorno fantastico", "tutto perfetto", "che giornata meravigliosa", "sono al settimo cielo" };
            string[] expressionsOfWorryKeywords = new string[] { "sono preoccupato", "che ansia", "mi sento inquieto", "ho delle preoccupazioni", "sono nervoso", "non sono tranquillo", "tutto mi preoccupa" };
            string[] expressionsOfRequestKeywords = new string[] { "mi dai un consiglio", "puoi aiutarmi", "ho bisogno di assistenza", "mi servirebbe un aiuto", "potresti guidarmi", "puoi dare una mano", "mi aiuti per favore" };
            string[] expressionsOfEnthusiasmKeywords = new string[] { "sono gasato", "sono carico", "non vedo l'ora", "che emozione", "non posso aspettare" };
            string[] expressionsOfHesitationKeywords = new string[] { "non sono sicuro", "ho dei dubbi", "forse", "potrebbe essere", "sono indeciso" };
            string[] expressionsOfComfortKeywords = new string[] { "andrà tutto bene", "non preoccuparti", "tranquillo", "ci sono per te", "ti capisco" };
            string[] expressionsOfExcitementKeywords = new string[] { "non vedo l'ora", "sono super eccitato", "questo è fantastico", "che figata", "sono al settimo cielo", "troppo bello", "spettacolare", "incredibile", "super entusiasta" };
            string[] expressionsOfConfidenceKeywords = new string[] { "sono sicuro", "ho fiducia", "non ho dubbi", "sono convinto", "conto su di esso", "certo di questo", "senza ombra di dubbio", "assolutamente sicuro", "pienamente convinto" };
            string[] yexpressionsOfGratitudeKeywords = new string[] { "grazie mille", "molto obbligato", "ti ringrazio", "apprezzo molto", "sei un tesoro", "grazie di cuore", "sei gentilissimo", "sei una benedizione", "sei il migliore", "non so cosa farei senza di te" };
            string[] expressionsOfSadnessKeywords = new string[] { "mi sento triste", "sono depresso", "giornata no", "mi sento giù", "tutto sbagliato", "non va nulla bene", "che giornata nera", "mi sento malissimo", "è tutto così difficile" };
            string[] expressionsOfSupportKeywords = new string[] { "mi serve aiuto", "puoi supportarmi", "ho bisogno di te", "non posso farcela da solo", "aiutami per favore", "mi serve un consiglio", "mi puoi guidare", "ho bisogno di un sostegno", "come posso fare" };
            string[] yexpressionsOfJoyKeywords = new string[] { "sono al settimo cielo", "non potrei essere più felice", "che gioia immensa", "sono pieno di felicità", "questo è il miglior giorno", "non mi sono mai sentito così bene", "tutto è perfetto", "la vita è bellissima", "non cambierei nulla" };
            string[] expressionsOfAnxietyKeywords = new string[] { "sono ansioso", "ho l'ansia", "mi sento teso", "sono stressato", "c'è troppa pressione", "mi sento sopraffatto", "non riesco a rilassarmi", "tutto mi preoccupa", "sono nervoso" };
            string[] yexpressionsOfReliefKeywords = new string[] { "che sollievo", "finalmente", "posso tirare un respiro", "meno male", "si è risolto tutto", "sono sollevato", "è andata bene", "la tensione è finita", "grande liberazione" };
            string[] expressionsOfDeterminationKeywords = new string[] { "non mi arrenderò", "continuerò a lottare", "ho deciso di andare avanti", "non mi fermerò", "continuerò a spingere", "sono determinato", "persevererò", "non cederò", "mantengo il corso", "non voltarmi indietro" };
            string[] yexpressionsOfCuriosityKeywords = new string[] { "sono curioso", "mi chiedo", "come mai", "perché", "puoi spiegare", "spiegami", "dimmi di più", "sono interessato a sapere", "ho una domanda" };
            string[] expressionsOfSatisfactionKeywords = new string[] { "sono soddisfatto", "questo è perfetto", "proprio quello che volevo", "completamente soddisfatto", "più di quanto mi aspettassi", "meglio di così non si può", "è esattamente ciò che cercavo", "sono pienamente contento" };
            string[] expressionsOfDiscontentKeywords = new string[] { "non sono contento", "questo non va bene", "non è quello che mi aspettavo", "sono deluso", "potrebbe essere meglio", "non è sufficiente", "lascia molto a desiderare", "sono insoddisfatto" };
            string[] yexpressionsOfEncouragementKeywords = new string[] { "dai che ce la fai", "su con la vita", "tira fuori il meglio", "non arrenderti", "tenere duro", "sempre avanti", "continua così", "forza e coraggio", "non mollare", "vinci le tue paure" };
            string[] expressionsOfApologyKeywords = new string[] { "mi scuso", "chiedo scusa", "sono spiacente", "non era mia intenzione", "mi dispiace", "scusami", "perdono", "è stato un errore", "non volevo", "mi pento" };
            string[] expressionsOfConfirmationKeywords = new string[] { "confermo", "certamente", "assolutamente", "esattamente", "proprio così", "senza dubbio", "sì esatto", "certo che sì", "indubbiamente", "decisamente" };
            string[] expressionsOfDisagreementKeywords = new string[] { "non sono d'accordo", "non penso sia così", "mi oppongo", "non è vero", "contrario a quello", "non condivido", "non mi convince", "non ci sto", "mi dissocio", "divergo" };
            string[] expressionsOfInterestKeywords = new string[] { "mi interessa", "sono interessato", "vorrei saperne di più", "attira la mia attenzione", "mi piacerebbe approfondire", "puoi dirmi di più", "sono curioso di questo", "vorrei esplorare", "mi affascina", "mi coinvolge" };
            string[] expressionsOfSurpriseKeywords = new string[] { "non me l'aspettavo", "che sorpresa", "davvero?", "non posso crederci", "incredibile", "questo è sorprendente", "mai avrei pensato", "come è possibile?", "sono sbalordito", "questo cambia tutto" };
            string[] expressionsOfRelaxationKeywords = new string[] { "ho bisogno di rilassarmi", "voglio rilassarmi", "è tempo di staccare", "devo decomprimere", "prendiamoci una pausa", "tempo per me", "bisogno di una pausa", "allentare la tensione", "rilassarsi un po'", "distendersi" };
            string[] expressionsOfDoubtKeywords = new string[] { "ho dei dubbi", "non sono sicuro", "può essere?", "sembra improbabile", "non mi convince", "sono indeciso", "è davvero così?", "puoi confermare?", "questo mi lascia perplesso", "posso fidarmi?" };
            string[] expressionsOfNeedKeywords = new string[] { "ho bisogno di aiuto", "mi serve supporto", "puoi assistere?", "necessito di guida", "è urgente", "mi serve una mano", "potresti aiutarmi?", "sto cercando aiuto", "ho bisogno di consigli", "aiuto richiesto" };
            string[] yexpressionsOfExcitementKeywords = new string[] { "sono eccitato", "che emozione", "non vedo l'ora", "tutto eccitante", "sono gasato", "è emozionante", "attesa febbrile", "sbalordito", "super eccitato", "pieno di aspettative" };
            string[] expressionsOfFrustrationKeywords = new string[] { "sono frustrato", "che seccatura", "sono stanco di questo", "non ne posso più", "è troppo", "mi irrita", "sono al limite", "non ci sto capendo nulla", "sono esasperato", "mi sento bloccato" };
            string[] expressionsOfGratefulnessKeywords = new string[] { "sono grato", "grazie infinite", "non so come ringraziarti", "sei stato così d'aiuto", "grazie di tutto", "non avrei potuto farcela senza di te", "ti sono debitore", "apprezzo molto il tuo aiuto", "grazie dal profondo del cuore", "sei un salvavita" };
            string[] expressionsOfContentmentKeywords = new string[] { "sono contento", "mi sento bene", "tutto va bene", "sono a posto così", "sono soddisfatto", "è tutto perfetto", "non potrebbe andare meglio", "sono tranquillo", "mi sento rilassato", "è una bella giornata" };
            string[] opportunityKeywords = new string[] { "opportunità", "possibilità", "chance", "occasione", "prospettiva" };
            string[] peaceKeywords = new string[] { "pace", "tranquillo", "serenità", "calmo", "rilassato" };
            string[] adventureKeywords = new string[] { "avventura", "esplorazione", "scoperta", "viaggio", "esplorare" };
            string[] celebrationKeywords = new string[] { "celebrazione", "festa", "onorare", "commemorare", "celebrare" };
            string[] growthKeywords = new string[] { "crescita", "sviluppo", "progredire", "evolvere", "maturare" };
            string[] innovationKeywords = new string[] { "innovazione", "nuove idee", "creatività", "inventare", "pionieristico" };
            string[] empowermentKeywords = new string[] { "empowerment", "potenziamento", "abilitazione", "forte", "autorizzazione" };
            string[] leadershipKeywords = new string[] { "leadership", "guida", "capo", "comando", "leader" };
            string[] harmonyKeywords = new string[] { "armonia", "equilibrio", "pace", "concordia", "simbiosi" };
            string[] wellnessKeywords = new string[] { "benessere", "salute", "vitalità", "fitness", "salubre" };
            string[] progressKeywords = new string[] { "progresso", "avanzamento", "sviluppo", "miglioramento", "innovazione" };
            string[] discoveryKeywords = new string[] { "scoperta", "esplorazione", "rivelazione", "scoprire", "trovare" };
            string[] joyKeywords = new string[] { "gioia", "felicità", "contentezza", "piacere", "soddisfazione" };
            string[] tranquilityKeywords = new string[] { "tranquillità", "pace", "serenità", "calma", "rilassamento" };
            string[] connectivityKeywords = new string[] { "connessione", "interazione", "networking", "comunicazione", "relazione" };
            string[] adaptabilityKeywords = new string[] { "adattabilità", "flessibilità", "adattarsi", "modificare", "versatilità" };
            string[] fulfillmentKeywords = new string[] { "realizzazione", "soddisfazione", "compiuto", "pienezza", "successo" };
            string[] wisdomKeywords = new string[] { "saggezza", "conoscenza", "intuito", "comprensione", "prudenza" };
            string[] energyKeywords = new string[] { "energia", "vitalità", "dinamismo", "forza", "vigore" };
            string[] ambitionKeywords = new string[] { "ambizione", "aspirazione", "obiettivo", "meta", "sogno" };
            string[] balanceKeywords = new string[] { "equilibrio", "bilanciamento", "armonia", "stabilità", "moderazione" };
            string[] relaxationTechniquesKeywords = new string[] { "tecniche di rilassamento", "meditazione guidata", "respirazione profonda", "yoga", "mindfulness" };
            string[] digitalDetoxKeywords = new string[] { "detox digitale", "disconnessione", "pausa tecnologica", "limitare lo smartphone", "uso consapevole della tecnologia" };
            string[] creativeWritingKeywords = new string[] { "scrittura creativa", "narrativa", "poesia", "diario personale", "scrittura terapeutica" };
            string[] lifeCoachingKeywords = new string[] { "coaching della vita", "consulenza personale", "guida", "supporto personale", "consigli per la vita" };
            string[] sustainableLivingKeywords = new string[] { "vita sostenibile", "ecosostenibilità", "ambiente", "stile di vita verde", "pratiche ecologiche" };
            string[] personalBrandingKeywords = new string[] { "branding personale", "immagine personale", "auto-marketing", "identità professionale", "costruzione del marchio personale" };
            string[] filmGenresKeywords = new string[] { "generi di film", "cinema d'azione", "film drammatici", "commedie", "film di fantascienza" };
            string[] movieNightsKeywords = new string[] { "serata film", "cinema a casa", "maratona di film", "notte di film", "visione collettiva" };
            string[] filmRecommendationsKeywords = new string[] { "consigli su film", "suggerimenti di film", "migliori film da vedere", "film consigliati", "film da non perdere" };
            string[] emotionalProcessingKeywords = new string[] { "elaborazione emotiva", "gestione delle emozioni", "sentimenti complessi", "intelligenza emotiva", "riflessione emotiva" };
            string[] selfIdentityKeywords = new string[] { "identità personale", "autoconsapevolezza", "definizione di sé", "scoperta di sé", "senso di sé" };
            string[] relationshipDynamicsKeywords = new string[] { "dinamiche relazionali", "problemi di relazione", "comunicazione interpersonale", "conflitti relazionali", "supporto nelle relazioni" };
            string[] mentalHealthAwarenessKeywords = new string[] { "consapevolezza della salute mentale", "benessere psicologico", "disturbi mentali", "supporto psicologico", "educazione alla salute mentale" };
            string[] anxietyManagementKeywords = new string[] { "gestione dell'ansia", "ridurre l'ansia", "tecniche di calma", "controllare l'ansia", "ansia cronica" };
            string[] lifeChangesKeywords = new string[] { "cambiamenti nella vita", "transizioni di vita", "nuovi inizi", "gestire il cambiamento", "adattarsi a nuove situazioni" };
            string[] stressReductionKeywords = new string[] { "riduzione dello stress", "gestione dello stress", "tecniche di rilassamento", "minimizzare lo stress", "stress cronico" };
            string[] personalBoundariesKeywords = new string[] { "limiti personali", "impostare confini", "rispetto dei confini", "autonomia personale", "protezione emotiva" };
            string[] overcomingTraumaKeywords = new string[] { "superare il trauma", "recupero dal trauma", "terapia per il trauma", "elaborazione del trauma", "guarigione post-traumatica" };
            string[] lifeSatisfactionKeywords = new string[] { "soddisfazione della vita", "contentezza", "realizzazione personale", "valutazione della vita", "qualità della vita" };
            string[] griefManagementKeywords = new string[] { "gestione del lutto", "superare il lutto", "elaborazione del dolore", "guarigione dal lutto", "affrontare la perdita" };
            string[] identityExplorationKeywords = new string[] { "esplorazione dell'identità", "scoperta di sé", "definizione di sé", "auto-riflessione", "crescita personale" };
            string[] personalResilienceKeywords = new string[] { "resilienza personale", "forza interiore", "recuperare dopo una caduta", "adattabilità", "superare le sfide" };
            string[] emotionalRegulationKeywords = new string[] { "regolazione emotiva", "controllo delle emozioni", "gestione delle emozioni", "equilibrio emotivo", "stabilità emozionale" };
            string[] relationshipBuildingKeywords = new string[] { "costruzione delle relazioni", "rafforzare le relazioni", "sviluppare legami", "relazioni sane", "intimità" };
            string[] stressCopingKeywords = new string[] { "coping dello stress", "strategie contro lo stress", "affrontare lo stress", "stress resilienza", "adattamento allo stress" };
            string[] personalEffectivenessKeywords = new string[] { "efficacia personale", "produttività personale", "autoefficacia", "miglioramento delle prestazioni", "realizzazione di obiettivi" };
            string[] healingProcessesKeywords = new string[] { "processi di guarigione", "recupero", "terapia di guarigione", "ripristino", "rigenerazione" };
            string[] decisionSupportKeywords = new string[] { "supporto decisionale", "prendere decisioni", "consulenza per decisioni", "orientamento nelle scelte", "aiuto nelle decisioni" };
            string[] selfWorthKeywords = new string[] { "valore personale", "autostima", "valutazione di sé", "amor proprio", "senso di valore" };
            string[] overcomingAddictionKeywords = new string[] { "superare la dipendenza", "recupero da dipendenze", "terapia di dipendenza", "liberarsi dalle dipendenze", "aiuto per dipendenze" };
            string[] mindfulnessPracticeKeywords = new string[] { "pratica della mindfulness", "meditazione mindfulness", "consapevolezza", "attenzione plena", "pratica meditativa" };
            string[] emotionalBalanceKeywords = new string[] { "equilibrio emotivo", "stabilità emotiva", "gestione delle emozioni", "armonia interiore", "calma emotiva" };
            string[] copingMechanismsKeywords = new string[] { "meccanismi di coping", "strategie di coping", "affrontare lo stress", "gestione dello stress", "adattamento emotivo" };
            string[] lifeGoalsPlanningKeywords = new string[] { "pianificazione degli obiettivi di vita", "definizione degli obiettivi", "sogni futuri", "mappatura del percorso di vita", "obiettivi a lungo termine" };
            string[] lifeManagementKeywords = new string[] { "gestione della vita", "organizzazione personale", "bilanciamento vita-lavoro", "gestione quotidiana", "ottimizzazione del tempo" };
            string[] traumaRecoveryKeywords = new string[] { "recupero dal trauma", "guarigione da traumi", "terapia post-traumatica", "superamento del trauma", "resilienza post-traumatica" };
            string[] personalIntegrityKeywords = new string[] { "integrità personale", "coerenza personale", "onestà con sé stessi", "valori personali", "etica personale" };
            string[] creativeExpressionKeywords = new string[] { "espressione creativa", "creatività artistica", "liberare la creatività", "arte e creatività", "esplorazione creativa" };
            string[] resilienceBuildingKeywords = new string[] { "costruzione della resilienza", "sviluppo della resilienza", "capacità di recupero", "forza interiore", "superare le avversità" };
            string[] overcomingAnxietyKeywords = new string[] { "superare l'ansia", "gestione dell'ansia", "tecniche di riduzione dell'ansia", "calmare l'ansia", "strategie contro l'ansia" };
            string[] dealingWithDepressionKeywords = new string[] { "affrontare la depressione", "superare la depressione", "strategie per la depressione", "aiuto per la depressione", "gestione della depressione" };
            string[] enhancingCreativityKeywords = new string[] { "potenziare la creatività", "sviluppare la creatività", "stimolare la creatività", "tecniche creative", "ispirazione creativa" };





            // Verifica se il messaggio contiene una delle parole chiave per il successo
            if (successKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il successo è il risultato di piccoli sforzi ripetuti giorno dopo giorno.",
                "Ogni piccolo successo è un passo verso il traguardo finale.",
                "Celebra ogni piccola vittoria, ogni passo conta.",
                "Il percorso verso il successo è lungo, ma ogni passo avanti è un progresso.",
                "Non sottovalutare i piccoli successi; sono le fondamenta dei grandi."
                };
            }
            else if (dealingWithDepressionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Affrontare la depressione richiede coraggio e supporto. Quali approcci hai trovato utili nel tuo percorso?",
                "Quali attività quotidiane aiutano a gestire i tuoi sintomi di depressione?",
                "Hai trovato particolarmente utile il supporto di gruppi o terapie? In che modo?",
                "Quali consigli daresti a qualcuno che sta iniziando a confrontarsi con la depressione?",
                "Come mantieni la speranza e l'ottimismo durante i periodi difficili legati alla depressione?"
                };
            }
            else if (enhancingCreativityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La creatività è essenziale in molti aspetti della vita. Come fai a coltivare la tua creatività quotidianamente?",
                "Quali tecniche usi per superare i blocchi creativi o per trovare ispirazione?",
                "C'è stato un progetto o un'attività recente che ha particolarmente stimolato la tua creatività?",
                "In che modo la creatività influisce sul tuo benessere emotivo o sul tuo lavoro?",
                "Quali risorse consiglieresti a qualcuno che desidera sviluppare o potenziare la propria creatività?"
                };
            }
            else if (overcomingAnxietyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'ansia può essere impegnativa. Quali strategie trovi più efficaci nel gestirla?",
                "Puoi condividere un'esperienza in cui hai applicato con successo una tecnica per ridurre l'ansia?",
                "Come valuti e scegli le tecniche più adatte a te per la gestione dell'ansia?",
                "Quali risorse o supporti consiglieresti a chi sta lottando contro l'ansia?",
                "In che modo il supporto di amici, familiari o professionisti ha influenzato il tuo percorso di gestione dell'ansia?"
                };
            }
            else if (resilienceBuildingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La resilienza è fondamentale per superare le sfide. Quali pratiche hai integrato nella tua vita per costruire resilienza?",
                "Qual è stato il momento in cui hai sentito di aver fatto affidamento sulla tua resilienza per superare un'ostacolo?",
                "Ci sono modelli o ispirazioni che segui per rafforzare la tua resilienza personale?",
                "Quali strategie consiglieresti a qualcuno che sta cercando di sviluppare una maggiore resilienza?",
                "Come ha influenzato la tua vita la capacità di adattarti e riprenderti dalle difficoltà?"
                };
            }
            else if (lifeManagementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Gestire efficacemente la vita richiede abilità. Quali strategie usi per organizzare la tua giornata?",
                "Come fai a mantenere un equilibrio tra lavoro e vita personale?",
                "Quali strumenti o metodi trovi più utili nell'ottimizzazione del tuo tempo?",
                "Hai affrontato recentemente delle sfide nella gestione della tua vita quotidiana? Come le hai superate?",
                "Qual è il tuo consiglio più utile per qualcuno che lotta con la gestione efficace della propria vita?"
                };
            }
            else if (traumaRecoveryKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il recupero dal trauma è un percorso personale. Quali risorse hai trovato particolarmente utili?",
                "Quali sono state le maggiori sfide nel tuo percorso di guarigione e come le hai affrontate?",
                "Hai consigli specifici per chi sta attraversando un processo di recupero da un trauma?",
                "In che modo il supporto degli altri ha giocato un ruolo nel tuo percorso di recupero?",
                "Come valuti i progressi nel tuo recupero dal trauma?"
                };
            }
            else if (personalIntegrityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Mantenere l'integrità personale richiede impegno. Come assicuri di rimanere fedele ai tuoi valori?",
                "Quali sfide hai incontrato nel mantenere la tua integrità personale?",
                "Come gestisci le situazioni che mettono alla prova la tua onestà o i tuoi valori personali?",
                "Hai esempi di come la tua integrità personale abbia influenzato positivamente la tua vita o le tue relazioni?",
                "Quali pratiche ti aiutano a mantenere una forte coerenza personale?"
                };
            }
            else if (creativeExpressionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Come trovi spazio e ispirazione per la tua espressione creativa?",
                "Quali forme di arte o creatività pratichi e cosa significano per te?",
                "C'è stato un progetto creativo di recente che ti ha particolarmente ispirato o sfidato?",
                "Come superi i blocchi creativi o i periodi di mancanza di ispirazione?",
                "Qual è l'impatto della creatività sulla tua salute emotiva o sul tuo benessere generale?"
                };
            }
            else if (lifeGoalsPlanningKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Pianificare gli obiettivi di vita può fornire direzione. Come stabilisci e rivedi i tuoi obiettivi a lungo termine?",
                "Quali tecniche utilizzi per rimanere in traccia verso il raggiungimento dei tuoi obiettivi di vita?",
                "Hai esempi di obiettivi che hai raggiunto di recente e come li hai conseguiti?",
                "Come bilanci la flessibilità e l'adattabilità nella pianificazione dei tuoi obiettivi di vita?",
                "Quali sfide hai incontrato nella pianificazione e realizzazione dei tuoi obiettivi e come le hai superate?"
                };
            }
            else if (emotionalBalanceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Mantenere l'equilibrio emotivo è cruciale. Quali pratiche quotidiane utilizzi per gestire le tue emozioni?",
                "Quali sfide hai incontrato nel tentativo di raggiungere una stabilità emotiva e come le hai superate?",
                "C'è stato un momento particolare in cui hai sentito di aver raggiunto un'armonia interiore significativa?",
                "Quali risorse o strumenti consideri più efficaci per mantenere la tua calma emotiva?",
                "Come affronti i momenti di grande stress o turbamento emotivo?"
                };
            }
            else if (copingMechanismsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "I meccanismi di coping sono vitali per la resilienza. Quali strategie di coping hai trovato più efficaci?",
                "Puoi condividere un esempio di come hai utilizzato un meccanismo di coping durante un periodo particolarmente stressante?",
                "Come valuti l'efficacia dei tuoi meccanismi di coping nel tempo? Hai fatto modifiche recentemente?",
                "Quali nuove strategie di coping vorresti esplorare o hai in programma di implementare?",
                "Come hai imparato a riconoscere quali meccanismi di coping funzionano meglio per te?"
                };
            }
            else if (healingProcessesKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "I processi di guarigione sono tanto unici quanto personali. Quali tecniche di guarigione hai trovato particolarmente efficaci?",
                "Qual è stata la tua esperienza con il recupero e il ripristino dopo un evento traumatico o una malattia?",
                "Credi che il supporto di amici, familiari o professionisti sia cruciale nel tuo processo di guarigione?",
                "Come definisci 'guarigione' nella tua vita personale e quali passi stai prendendo per raggiungerla?",
                "Quali sono state le maggiori sfide che hai incontrato nel tuo percorso di guarigione e come le hai superate?"
                };
            }
            else if (decisionSupportKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Prendere decisioni può essere difficile. Come ti orienti nelle scelte importanti della tua vita?",
                "Hai un processo o un metodo specifico che utilizzi per prendere decisioni complesse?",
                "Qual è stata una decisione significativa che hai preso recentemente e come sei stato supportato nel processo?",
                "Credi nell'importanza di cercare consigli esterni quando devi prendere decisioni importanti?",
                "Come gestisci l'ansia che può accompagnare le grandi decisioni?"
                };
            }
            else if (selfWorthKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il senso di valore personale è fondamentale per il benessere. Come lavori per mantenere o migliorare il tuo valore personale?",
                "Quali esperienze hai avuto che hanno significativamente influenzato la tua autostima?",
                "Hai strategie per affrontare i momenti in cui il tuo senso di valore personale è messo alla prova?",
                "In che modo le relazioni con gli altri influenzano il tuo senso di autostima?",
                "Qual è il ruolo del riconoscimento esterno nella tua percezione del valore personale?"
                };
            }
            else if (overcomingAddictionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Superare una dipendenza è un viaggio impegnativo. Quali risorse o supporti hai trovato utili nel tuo processo di recupero?",
                "Quali sono stati gli ostacoli principali nel tuo percorso di liberazione dalle dipendenze e come li hai affrontati?",
                "In che modo il supporto professionale ha giocato un ruolo nel tuo percorso di recupero da una dipendenza?",
                "Quali cambiamenti nello stile di vita hai implementato per supportare il tuo recupero da dipendenze?",
                "Come mantieni la tua motivazione e impegni nella lotta contro le dipendenze a lungo termine?"
                };
            }
            else if (mindfulnessPracticeKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La mindfulness è una pratica potente. Come l'hai integrata nella tua routine quotidiana?",
                "Quali benefici hai notato dal praticare la mindfulness o la meditazione?",
                "Hai consigli specifici per qualcuno che sta cercando di iniziare o migliorare la sua pratica di mindfulness?",
                "Quali sfide hai incontrato nell'approfondire la tua pratica meditativa e come le hai superate?",
                "C'è un particolare esercizio di mindfulness che trovi particolarmente utile o rivelatore?"
                };
            }

            else if (stressCopingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Ognuno ha metodi unici per affrontare lo stress. Quali sono i tuoi e come li hai sviluppati?",
                "Ci sono strategie specifiche che hai trovato particolarmente efficaci contro lo stress cronico?",
                "Come mantieni la tua resilienza quando ti trovi di fronte a stress intensi o prolungati?",
                "Qual è stata una situazione di stress recente e come l'hai gestita?",
                "C'è un consiglio pratico che ti sentiresti di condividere su come gestire meglio lo stress quotidiano?"
                };
            }
            else if (personalEffectivenessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Migliorare l'efficacia personale è fondamentale per raggiungere gli obiettivi. Quali tattiche hai trovato utili?",
                "Come valuti e misuri la tua produttività personale e quali strumenti utilizzi per migliorarla?",
                "Quali ostacoli hai dovuto superare per diventare più efficace nel tuo lavoro o nella tua vita personale?",
                "Hai esempi specifici di come hai migliorato la tua capacità di realizzare obiettivi recentemente?",
                "Quali sono i principi chiave che segui per mantenere alta la tua autoefficacia?"
                };
            }
            else if (emotionalRegulationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La regolazione emotiva è essenziale per una vita equilibrata. Quali tecniche usi per gestire le tue emozioni?",
                "Hai risorse o attività che ti aiutano a mantenere la stabilità emozionale nei momenti difficili?",
                "Come valuti l'impatto della tua capacità di regolare le emozioni sulle tue relazioni personali e professionali?",
                "Quali sfide hai incontrato nel tentativo di migliorare il controllo delle tue emozioni?",
                "C'è un momento specifico in cui hai riconosciuto un significativo miglioramento nella tua regolazione emotiva?"
                };
            }
            else if (relationshipBuildingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Costruire relazioni sane è un arte. Quali qualità ritieni più importanti nello sviluppo di legami forti?",
                "Come affronti la costruzione di nuove relazioni in vari ambiti della tua vita, come lavoro o tempo libero?",
                "Hai esempi di come hai rafforzato una relazione importante recentemente?",
                "Quali sfide hai riscontrato nel cercare di creare intimità e come le hai superate?",
                "Che consigli daresti a qualcuno che vuole migliorare la qualità delle proprie relazioni?"
                };
            }
            else if (griefManagementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Affrontare un lutto è un processo profondamente personale. Quali sono state le tue strategie principali per gestire il dolore?",
                "Come ha influenzato il supporto di amici o professionisti il tuo percorso di elaborazione del lutto?",
                "Esistono particolari attività o pratiche che ti hanno aiutato a trovare conforto durante i periodi di lutto?",
                "Quali lezioni hai imparato su te stesso e sulla vita mentre affrontavi la perdita?",
                "Come descriveresti il tuo processo di guarigione dal lutto fino ad ora?"
                };
            }
            else if (identityExplorationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'esplorazione dell'identità può essere un viaggio illuminante. Quali aspetti di te stesso scopri di più recentemente?",
                "In che modo le esperienze di vita hanno influenzato la tua percezione e definizione di te stesso?",
                "Hai particolari metodi o pratiche che ti aiutano nell'auto-riflessione e nella scoperta di sé?",
                "Come valuti i cambiamenti nella tua identità nel corso degli anni?",
                "Quali sfide hai incontrato nel cercare di definire chi sei e come le hai superate?"
                };
            }
            else if (personalResilienceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La resilienza è cruciale per superare le sfide. Puoi condividere un'esperienza che dimostri la tua resilienza personale?",
                "Quali risorse trovi più efficaci per recuperare da situazioni difficili o fallimenti?",
                "In che modo sviluppi e mantieni la tua forza interiore di fronte alle avversità?",
                "Hai consigli per qualcuno che sta cercando di costruire o rafforzare la propria resilienza?",
                "Quali sono stati alcuni degli ostacoli più grandi che hai superato e che hanno rafforzato la tua resilienza?"
                };
            }
            else if (lifeSatisfactionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Come valuti la tua attuale soddisfazione della vita e quali fattori contribuiscono maggiormente ad essa?",
                "C'è un aspetto della tua vita che senti abbia maggiormente bisogno di miglioramento per aumentare la tua soddisfazione?",
                "Quali momenti o decisioni ritieni abbiano avuto l'impatto più significativo sulla tua qualità di vita?",
                "In che modo le tue aspirazioni e i tuoi valori influenzano la tua percezione della contentezza nella vita?",
                "Quali sono le strategie che utilizzi per mantenere o migliorare la tua soddisfazione personale nel tempo?"
                };
            }
            else if (overcomingTraumaKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il processo di guarigione da un trauma può essere lungo e difficile. Quali sono state le risorse più utili per te in questo percorso?",
                "Come ha influenzato la terapia il tuo processo di recupero dal trauma?",
                "Quali strategie specifiche hai trovato efficaci nell'elaborazione delle esperienze traumatiche?",
                "Puoi condividere un insight o un momento chiave nel tuo percorso di guarigione post-traumatica?",
                "Quali cambiamenti hai notato in te stesso dall'inizio del tuo percorso di superamento del trauma?"
                };
            }
            else if (stressReductionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Ridurre lo stress è essenziale per mantenere una buona salute. Quali tecniche di rilassamento trovi più efficaci?",
                "Come gestisci lo stress quotidiano? Ci sono attività o pratiche specifiche che ti aiutano a rimanere calmo?",
                "Quali sono le tue strategie per minimizzare lo stress nelle situazioni ad alta pressione?",
                "Lo stress cronico può essere debilitante. Come ti assicuri di affrontare questo problema in modo proattivo?",
                "Hai ricevuto consigli utili su come gestire lo stress che vorresti condividere?"
                };
            }
            else if (personalBoundariesKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Impostare limiti personali è cruciale per il benessere psicologico. Come definisci e mantieni i tuoi confini?",
                "Hai trovato difficoltà nell'impostare o mantenere confini chiari con gli altri? Come hai gestito queste situazioni?",
                "Qual è stata una situazione in cui hai sentito che i tuoi limiti personali sono stati rispettati o protetti in modo efficace?",
                "Come reagisci quando qualcuno non rispetta i tuoi confini personali?",
                "Quali consigli hai per qualcuno che sta cercando di migliorare nel definire e proteggere i suoi limiti personali?"
                };
            }
            else if (anxietyManagementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La gestione dell'ansia è cruciale per mantenere un equilibrio nella vita. Quali tecniche ti aiutano a gestire l'ansia efficacemente?",
                "Hai trovato particolarmente utile qualche forma di terapia o consulenza per controllare l'ansia?",
                "Quali sono i trigger che noti che aggravano la tua ansia e come li affronti?",
                "In che modo l'ansia influisce sulla tua vita quotidiana e quali passi stai prendendo per ridurne l'impatto?",
                "Condividi un consiglio che ti è stato particolarmente utile nella gestione dell'ansia cronica."
                };
            }
            else if (lifeChangesKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "I cambiamenti nella vita possono essere sia eccitanti che spaventosi. Come ti prepari a importanti transizioni di vita?",
                "Quali strategie trovi efficaci per adattarti a nuove situazioni o ambienti?",
                "Hai un esempio di un recente grande cambiamento nella tua vita e come lo hai gestito?",
                "In che modo cerchi supporto durante i periodi di transizione significativi nella tua vita?",
                "Qual è stata la transizione più difficile che hai affrontato e cosa hai imparato da essa?"
                };
            }
            else if (mentalHealthAwarenessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La consapevolezza della salute mentale è fondamentale nella nostra società. Qual è la tua percezione della sensibilizzazione attuale su questi temi?",
                "Il supporto psicologico può essere un cambiamento di vita. Qual è stata la tua esperienza personale (se disponibile a condividerla) con la terapia o altri supporti psicologici?",
                "Come pensi che la società potrebbe migliorare nella gestione dei disturbi mentali e nel supporto alle persone affette?",
                "Quali risorse consideri essenziali per educare le persone sulla salute mentale?",
                "Hai consigli o risorse che potresti consigliare a qualcuno che sta cercando di migliorare la propria salute mentale o di supportare un'altra persona?"
                };
            }
            else if (emotionalProcessingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Elaborare le proprie emozioni è un passo fondamentale per il benessere psicologico. Qual è la tua strategia principale per gestire emozioni intense o complesse?",
                "L'intelligenza emotiva è una risorsa preziosa nella vita quotidiana. Come credi di aver sviluppato questa capacità nel tempo?",
                "Riflettere sulle proprie emozioni può portare a importanti intuizioni. Qual è stata una recente scoperta che hai fatto su di te attraverso la riflessione emotiva?",
                "Credi che il modo in cui gestisci le tue emozioni abbia cambiato il modo in cui interagisci con gli altri? Come?",
                "Gestire emozioni complesse può essere impegnativo. Qual è stata una situazione recente che ti ha messo alla prova, e come hai reagito?"
                };
            }
            else if (selfIdentityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'identità personale è un tema centrale in molte discussioni psicologiche. Come descriveresti il tuo processo di scoperta di sé?",
                "L'autoconsapevolezza può influenzare molti aspetti della vita. Qual è stato un momento significativo di crescita personale per te?",
                "Come pensi che il tuo senso di sé sia cambiato nel corso del tempo? Ci sono stati eventi o persone che consideri determinanti in questo processo?",
                "Affrontare questioni di identità può essere complesso. Quali sfide hai incontrato nel definire te stesso e come le hai superate?",
                "Cosa significa per te avere una forte identità personale, e come la mantieni o la rafforzi?"
                };
            }
            else if (relationshipDynamicsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Navigare nelle dinamiche relazionali può essere complicato. Qual è il tuo approccio principale nel gestire i conflitti nelle relazioni?",
                "La comunicazione è vitale nelle relazioni. Hai strategie specifiche che utilizzi per migliorare la comunicazione con le persone importanti nella tua vita?",
                "Affrontare problemi di relazione richiede spesso supporto esterno. In che modo le consultazioni con un terapeuta o un consigliere hanno aiutato le tue relazioni?",
                "Qual è stata la sfida più grande che hai affrontato in una relazione importante e come l'hai superata?",
                "Come assicuri che le tue relazioni rimangano sane e supportive? Hai delle tecniche o pratiche particolari che trovi efficaci?"
                };
            }

            else if (lifeCoachingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il coaching della vita può essere un potente strumento di cambiamento. Qual è il miglior consiglio che hai ricevuto da un life coach?",
                "La consulenza personale spesso rivela nuove prospettive. Quali sono stati alcuni degli insight più significativi per te?",
                "La guida di un coach può aiutare a navigare momenti difficili. Hai esperienze in cui il coaching ti ha aiutato particolarmente?",
                "Come pensi che il supporto personale possa migliorare la tua vita quotidiana?",
                "I consigli per la vita che si ricevono possono trasformare le nostre azioni quotidiane. Qual è un consiglio che ha avuto un impatto duraturo su di te?"
                };
            }
            else if (sustainableLivingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Vivere in modo sostenibile è fondamentale per il nostro pianeta. Quali sono alcune pratiche ecologiche che hai adottato nella tua vita?",
                "L'adozione di uno stile di vita verde può essere sia gratificante che sfidante. Quali sono state le tue esperienze in questo percorso?",
                "Come pensi che le piccole azioni quotidiane possano contribuire alla sostenibilità ambientale?",
                "Esplorare la sostenibilità nel proprio stile di vita può essere un'avventura. Hai scoperto nuovi prodotti o tecnologie ecologiche recentemente?",
                "Qual è la tua motivazione principale per seguire uno stile di vita ecologico e quali benefici hai notato?"
                };
            }
            else if (personalBrandingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il branding personale è essenziale in molti campi oggi. Come definiresti il tuo brand personale?",
                "L'auto-marketing può aprire molte porte professionalmente. Quali strategie usi per promuovere la tua immagine personale?",
                "Costruire un'identità professionale forte può essere un grande vantaggio. Quali elementi ritieni fondamentali per un efficace branding personale?",
                "Come assicuri che il tuo marchio personale rifletta autenticamente chi sei e i tuoi valori fondamentali?",
                "Quali sfide hai incontrato nel costruire il tuo marchio personale e come le hai superate?"
                };
            }

            else if (relaxationTechniquesKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Le tecniche di rilassamento possono migliorare significativamente la qualità della vita. Qual è la tua tecnica preferita?",
                "La meditazione guidata è un ottimo modo per calmare la mente. Pratichi regolarmente?",
                "La respirazione profonda è una pratica semplice ma potente. Hai notato miglioramenti dal suo utilizzo regolare?",
                "Il yoga non solo rilassa, ma aiuta anche a mantenere il corpo in forma. Qual è il tuo stile di yoga preferito?",
                "La mindfulness aiuta a vivere il momento. Come incorpori la mindfulness nella tua routine quotidiana?"
                };
            }
            else if (digitalDetoxKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Un detox digitale può essere liberatorio. Hai già provato a fare una pausa tecnologica?",
                "Limitare l'uso dello smartphone è importante per la salute mentale. Quali strategie usi per ridurne l'utilizzo?",
                "Fare pause regolari dalla tecnologia può aiutare a riconnettersi con il mondo reale. Cosa ti piace fare durante queste pause?",
                "Come gestisci l'equilibrio tra il tempo online e offline nella tua vita quotidiana?",
                "Pensi che il detox digitale possa migliorare la concentrazione e ridurre lo stress? Qual è stata la tua esperienza?"
                };
            }
            else if (creativeWritingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La scrittura creativa è un ottimo sfogo per l'espressione personale. Che tipo di scrittura ti piace fare?",
                "Narrare storie può essere tanto divertente quanto terapeutico. Stai lavorando su qualche progetto di scrittura in questo periodo?",
                "La poesia è una forma d'arte profonda. Scrivi poesie? Cosa ti ispira?",
                "Tenere un diario personale è un'abitudine che molti trovano utile. Hai un diario dove annoti i tuoi pensieri e le tue esperienze?",
                "La scrittura può essere usata anche come terapia. Hai mai sperimentato benefici emotivi o psicologici dalla scrittura?"
                };
            }

            else if (ambitionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'ambizione può spingere al successo. Qual è il tuo obiettivo principale al momento?",
                "Avere grandi aspirazioni è il primo passo per realizzare i propri sogni. Cosa aspiri a raggiungere nel prossimo futuro?",
                "Ogni grande traguardo inizia con un sogno. Hai un sogno che stai cercando di trasformare in realtà?",
                "Impostare obiettivi chiari è cruciale per il successo. Quali sono le mete che ti sei posto di recente?",
                "Le ambizioni guidano la nostra motivazione. Come mantieni alta la tua motivazione verso i tuoi obiettivi?"
                };
            }
            else if (balanceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Mantenere l'equilibrio nella vita è essenziale. Come fai a bilanciare lavoro e tempo libero?",
                "L'armonia tra le varie aree della vita porta benessere. Quali strategie utilizzi per mantenere questo equilibrio?",
                "La stabilità è fondamentale sia nella vita personale che professionale. Cosa fai per mantenere una buona stabilità nelle tue giornate?",
                "Trovare un giusto equilibrio può essere una sfida. Hai consigli su come raggiungere e mantenere la moderazione in tutto ciò che fai?",
                "L'equilibrio è spesso una danza delicata. Qual è stato il tuo più grande successo nel trovare armonia nella tua vita?"
                };
            }
            else if (wisdomKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La saggezza è il frutto dell'esperienza. Qual è il consiglio più saggio che hai mai ricevuto?",
                "Acquisire conoscenza è un processo continuo. Qual è stata l'ultima lezione importante che hai appreso?",
                "Spesso l'intuito ci guida nelle decisioni. Puoi raccontare un'occasione in cui il tuo intuito si è rivelato corretto?",
                "La comprensione profonda di un argomento può essere molto gratificante. Su quale argomento ti senti particolarmente competente?",
                "Come fai ad applicare la prudenza nelle tue scelte quotidiane? Hai degli esempi specifici di scelte prudenti che hai fatto di recente?"
                };
            }
            else if (energyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Avere energia è fondamentale per affrontare le sfide. Cosa fai per mantenerti energico e attivo?",
                "La vitalità è tanto fisica quanto mentale. Quali sono le tue attività preferite per mantenere alto il tuo livello di energia?",
                "Il dinamismo può essere contagioso. Come riesci a infondere energia e entusiasmo nelle persone intorno a te?",
                "La forza non è solo fisica, ma anche mentale. Come rafforzi la tua resilienza e la tua forza interiore?",
                "Mantenere il vigore richiede un equilibrio. Come bilanci lavoro, riposo e tempo libero per mantenere la tua energia ottimale?"
                };
            }
            else if (adaptabilityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'adattabilità è una competenza chiave oggi. Come ti adatti ai cambiamenti nella tua vita o lavoro?",
                "Essere flessibili può aiutare a superare molte sfide. Qual è stata la tua esperienza più recente dove la flessibilità è stata fondamentale?",
                "Adattarsi alle nuove situazioni è un'arte. Hai consigli su come migliorare questa capacità?",
                "La versatilità è una virtù in molti ambiti. In quali situazioni hai dovuto essere particolarmente versatile ultimamente?",
                "Adattarsi non è sempre facile. Qual è stato il cambiamento più difficile a cui ti sei dovuto adattare recentemente?"
                };
            }
            else if (fulfillmentKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Trovare la realizzazione personale è essenziale. Che cosa ti dà più soddisfazione nella vita?",
                "Sentirsi compiuti può derivare da molte fonti. Qual è stato il tuo successo più gratificante ultimamente?",
                "La realizzazione può venire dal lavoro, dalla famiglia, dagli hobby. Dove trovi la tua pienezza?",
                "Avere successo in qualcosa che ci appassiona è molto gratificante. Qual è il tuo obiettivo attuale per sentirti realizzato?",
                "La soddisfazione personale è un indicatore di una vita ben vissuta. Quali sono le cose che ti rendono veramente compiuto?"
                };
            }
            else if (tranquilityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La tranquillità è un tesoro. Come trovi la pace nella tua vita quotidiana?",
                "Vivere in serenità è un obiettivo nobilissimo. Quali pratiche ti aiutano a mantenere la calma?",
                "A volte, un momento di calma può fare la differenza. Qual è il tuo luogo preferito per rilassarti e trovare serenità?",
                "Cerchi sempre di creare un ambiente tranquillo attorno a te? Quali sono i tuoi metodi preferiti per farlo?",
                "La pace interiore porta benefici immensi. Hai consigli su come coltivarla giorno dopo giorno?"
                };
            }
            else if (connectivityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Vivere in un mondo connesso offre infinite opportunità. Come valorizzi le tue connessioni personali e professionali?",
                "La comunicazione efficace è la chiave delle buone relazioni. Hai qualche suggerimento su come migliorare l'interazione con gli altri?",
                "Networking è un'arte. Quali strategie trovi più efficaci per costruire e mantenere relazioni significative?",
                "Nell'era digitale, restare connessi è più semplice ma anche più complesso. Come gestisci le tue relazioni online?",
                "La connessione umana è essenziale per il benessere. Quali attività ti aiutano a sentirti più connesso con gli altri?"
                };
            }
            else if (discoveryKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La scoperta è il cuore dell'apprendimento. Qual è stata la tua ultima grande scoperta?",
                "Esplorare nuovi orizzonti può essere tanto eccitante. Cosa hai esplorato di recente?",
                "Ogni rivelazione porta nuova conoscenza. C'è stata qualche scoperta recente che ha cambiato il tuo modo di pensare?",
                "Scoprire qualcosa di nuovo è sempre entusiasmante. Qual è stata la tua ultima sorpresa piacevole?",
                "Il piacere di trovare qualcosa di inaspettato è unico. Hai trovato qualcosa di inatteso ultimamente?"
                };
            }
            else if (joyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Condividere momenti di gioia rende tutto più bello. Qual è stata la tua recente fonte di felicità?",
                "La felicità si trova spesso nelle piccole cose. Cosa ti ha reso particolarmente contento ultimamente?",
                "Celebrare i piaceri della vita può essere molto gratificante. Hai avuto qualche soddisfazione personale da condividere?",
                "La gioia di vivere è contagiosa. Che cosa ti rende felice quotidianamente?",
                "Trovare soddisfazione nelle proprie attività è fondamentale. Qual è stata la tua ultima attività che ti ha dato grande piacere?"
                };
            }
            else if (wellnessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Mantenere il benessere è fondamentale per una vita felice. Quali abitudini salutari pratichi?",
                "La salute è la vera ricchezza. Hai integrato nuove pratiche salubri nella tua routine di recente?",
                "Il fitness è tanto fisico quanto mentale. Come bilanci questi aspetti nel tuo percorso verso il benessere?",
                "Una vita salubre migliora ogni giorno. Quali cambiamenti hai fatto per promuovere la tua salute?",
                "La vitalità si coltiva con buone abitudini. Cosa ti fa sentire più vivo e in salute?"
                };
            }
            else if (progressKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il progresso è il motore della crescita personale. In quale area della tua vita stai vedendo miglioramenti?",
                "L'avanzamento richiede impegno costante. Quali passi hai fatto di recente per sviluppare le tue competenze o la tua carriera?",
                "Il miglioramento continuo è un eccellente obiettivo di vita. Quali successi hai raggiunto ultimamente?",
                "L'innovazione nel proprio campo può essere molto gratificante. Che novità stai esplorando ora?",
                "Il progresso può essere lento e costante. Come mantieni la motivazione durante i periodi di lento sviluppo?"
                };
            }
            else if (harmonyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Vivere in armonia è essenziale per il benessere. Come mantieni l'equilibrio nella tua vita?",
                "L'armonia nella vita quotidiana porta pace. Che tecniche utilizzi per preservare la calma interiore?",
                "La concordia tra lavoro e vita privata è fondamentale. Hai trovato il tuo equilibrio ideale?",
                "Raggiungere la simbiosi perfetta in ogni aspetto della vita è un'arte. Qual è il tuo segreto per una vita armoniosa?",
                "L'equilibrio è la chiave della stabilità. Quali sono i tuoi metodi preferiti per mantenere l'armonia personale e professionale?"
                };
            }
            else if (leadershipKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La leadership è più di una posizione, è un'azione. Che tipo di leader aspiri ad essere?",
                "Essere un buon capo significa ispirare gli altri. Come motivi il tuo team?",
                "La guida efficace richiede coraggio e umiltà. Quali sono le tue principali strategie di leadership?",
                "Ogni leader ha il suo stile unico. Quali qualità pensi siano essenziali per un buon leader?",
                "La leadership può essere tanto gratificante quanto impegnativa. Quali sfide hai affrontato come leader?"
                };
            }
            else if (innovationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'innovazione spinge il mondo in avanti. A quale nuova idea stai lavorando?",
                "Creare qualcosa di nuovo è sempre eccitante. Quali sono le tue ultime invenzioni?",
                "Pensare in modo creativo apre infinite possibilità. Quali sono state le tue recenti ispirazioni?",
                "Essere pionieristici nel tuo campo può essere tanto sfidante quanto gratificante. Qual è stato il tuo ultimo progetto innovativo?",
                "L'innovazione è la chiave per il futuro. Come pensi di contribuire con le tue idee creative?"
                };
            }
            else if (empowermentKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi potenziati è fondamentale per l'autoefficacia. In che modo ti senti abilitato nella tua vita?",
                "L'empowerment personale trasforma la nostra realtà. Quali passi hai intrapreso per diventare più forte?",
                "Avere il controllo della propria vita è liberatorio. Cosa fa sentire te più potente e in controllo?",
                "Essere autorizzati a fare scelte importanti è cruciale. Come prendi decisioni importanti?",
                "Il potenziamento può venire da molti aspetti della vita. Qual è stata la tua esperienza più significativa di empowerment?"
                };
            }
            else if (growthKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La crescita personale è un viaggio senza fine. Che passi hai fatto di recente verso il tuo sviluppo?",
                "Progredire ogni giorno è un grande traguardo! Cosa ti ha aiutato a evolvere ultimamente?",
                "Maturare non è sempre facile, ma è sempre gratificante. Hai scoperto qualcosa di nuovo su di te?",
                "Lo sviluppo personale spesso porta sfide. Come stai affrontando il tuo percorso di crescita?",
                "Ogni piccolo passo verso la crescita conta. Quali cambiamenti positivi hai notato in te stesso?"
                };
            }
            else if (celebrationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Le celebrazioni portano gioia e connessione. C'è un evento speciale che stai festeggiando?",
                "Onorare i momenti significativi è importante. Cosa stai commemorando?",
                "Feste e celebrazioni possono essere così vivaci! Hai qualcosa in particolare che stai celebrando?",
                "Ogni celebrazione ha una storia. Qual è l'occasione speciale per te in questo periodo?",
                "Celebrare le vittorie, grandi e piccole, è essenziale. Qual è stata la tua ultima celebrazione?"
                };
            }
            else if (peaceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La tranquillità è la chiave per una vita serena. Come mantieni la tua pace interiore?",
                "Essere calmo e rilassato è essenziale. Hai dei consigli su come mantenere questa serenità?",
                "La pace è preziosa, soprattutto nel mondo frenetico di oggi. Cosa ti aiuta a rimanere tranquillo?",
                "Sembra che tu abbia trovato il tuo angolo di serenità. Che cosa ti rende così pacifico oggi?",
                "Rilassarsi e lasciar andare lo stress è vitale. Quali sono le tue tecniche preferite per rilassarti?"
                };
            }
            else if (adventureKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Le avventure rendono la vita eccitante! Qual è la tua prossima esplorazione?",
                "Esplorare nuovi posti può essere così gratificante. Dove ti ha portato il tuo ultimo viaggio?",
                "Scoprire l'ignoto è sempre avvincente. Hai nuove avventure in programma?",
                "Ogni viaggio è una storia. Raccontami di una recente avventura che hai vissuto.",
                "Essere un esploratore nel mondo o nella vita è entusiasmante. Qual è stata la tua scoperta più recente?"
                };
            }
            else if (opportunityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Le opportunità sono spesso il primo passo verso grandi successi!",
                "Ogni occasione può trasformarsi in qualcosa di meraviglioso.",
                "Una nuova possibilità? Suona eccitante, esploriamola insieme!",
                "Le prospettive future sono sempre stimolanti, hai già qualche idea?",
                "Prendere al volo una chance può cambiare tutto, parliamone."
                };
            }
            else if (expressionsOfContentmentKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "È meraviglioso sapere che ti senti bene!",
                "Che bello che tutto vada bene per te. Continua a goderti questa bella giornata.",
                "Sono contento di sentire che sei soddisfatto. C'è qualcosa che vorresti aggiungere per rendere la giornata ancora migliore?",
                "La tranquillità è preziosa. È bello sapere che ti senti rilassato.",
                "Mantenere un senso di contentezza è importante. Continua a fare ciò che ti rende felice!"
                };
            }
            else if (yexpressionsOfExcitementKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "È fantastico vedere il tuo entusiasmo! Cosa ti rende così eccitato?",
                "Che bello che sei pieno di emozione! Qual è l'evento che aspetti con impazienza?",
                "L'emozione è contagiosa! Mi fa piacere che tu sia così felice.",
                "Sembra che ci sia qualcosa di veramente eccitante all'orizzonte per te!",
                "Ricorda di goderti ogni momento di questa attesa eccitante!"
                };
            }
            else if (expressionsOfFrustrationKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Mi dispiace sentirti così frustrato. Vuoi parlare di cosa sta causando questo stress?",
                "La frustrazione può essere difficile da gestire. Come posso aiutarti a alleviare un po' di questa tensione?",
                "A volte, condividere il proprio carico può aiutare. Raccontami di più su ciò che ti sta frustrando.",
                "Capisco che può essere sovrastante. C'è qualcosa di specifico che posso fare per te ora?",
                "Esasperato è un sentimento pesante. Cerchiamo insieme una soluzione."
                };
            }
            else if (expressionsOfGratefulnessKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Sono davvero toccato dal tuo ringraziamento. È sempre un piacere aiutarti!",
                "Grazie per le tue gentili parole. Sono qui per aiutarti quando ne hai bisogno.",
                "L'apprezzamento è sempre benvenuto! Non esitare a chiedere se c'è altro che posso fare per te.",
                "È gratificante sapere che sono stato di aiuto. Continua a contare su di me!",
                "Essere chiamato un salvavita è un grande onore. Grazie per affidarti a me!"
                };
            }

            else if (expressionsOfRelaxationKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Prendersi un momento per rilassarsi è importante. Hai già pensato a come vorresti farlo?",
                "Rilassarsi può fare meraviglie. Che ne dici di ascoltare un po' di musica tranquilla o fare una passeggiata?",
                "È essenziale allentare la tensione di tanto in tanto. Vuoi qualche suggerimento su attività rilassanti?",
                "Prendiamoci una pausa, allora! Qualche minuto di quiete può aiutarti a ricaricarti.",
                "Tempo per te è una scelta saggia. Considera di praticare mindfulness o yoga per un effetto rilassante.",
                "Capisco il bisogno di distendersi. Forse un libro o un film potrebbero essere un buon modo per staccare."
                };
            }
            else if (expressionsOfDoubtKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "I dubbi sono parte del processo di apprendimento. Qual è la tua specifica preoccupazione?",
                "Se non sei sicuro, possiamo esaminare insieme le informazioni per aiutarti a chiarire.",
                "È naturale avere dubbi. Cerchiamo di capire meglio insieme.",
                "Posso aiutarti a confermare qualsiasi informazione. Che cosa ti lascia perplesso?",
                "Capisco che la situazione possa sembrare improbabile. Vuoi esplorare altre opinioni o fonti?",
                "La sicurezza è importante. Dimmi cosa ti serve per sentirti più sicuro e fiducioso."
                };
            }
            else if (expressionsOfNeedKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Sono qui per aiutarti. Qual è il problema e come posso assisterti?",
                "Capisco che sia urgente. Concentriamoci su cosa possiamo fare subito per risolvere la situazione.",
                "Mi dispiace che tu stia attraversando un momento difficile. Parliamo di come posso supportarti.",
                "L'aiuto è a portata di mano. Descrivimi cosa ti serve e io farò del mio meglio per assisterti.",
                "Se hai bisogno di consigli, sono pronto a fornirli. Qual è la questione su cui hai bisogno di orientamento?",
                "Non esitare a chiedere aiuto. Qual è la tua richiesta specifica?"
                };
            }

            else if (expressionsOfDisagreementKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Capisco che possiamo vedere le cose diversamente. Vuoi discuterne per capire meglio i nostri punti di vista?",
                "È importante esprimere il proprio disaccordo. Quali sono i tuoi argomenti?",
                "Dissensi possono portare a conversazioni costruttive. Esploriamo le tue preoccupazioni.",
                "Non essere d'accordo è parte del dialogo. Come posso aiutarti a chiarire questi punti?",
                "Riconoscere le nostre differenze può aiutarci a comprendere meglio. Dimmi di più sul tuo punto di vista.",
                "È normale non essere sempre d'accordo. Possiamo approfondire insieme questa questione?"
                };
            }
            else if (expressionsOfInterestKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Che bello che tu sia interessato! Su quale aspetto vorresti avere più informazioni?",
                "Sono qui per aiutarti a esplorare questo argomento. Che cosa ti interessa esattamente?",
                "L'interesse è il primo passo verso l'apprendimento. Parliamone più dettagliatamente.",
                "Felicissimo di approfondire questo tema per te. Quali sono le tue domande specifiche?",
                "L'entusiasmo per un nuovo argomento è sempre eccitante! Iniziamo con qualche informazione base.",
                "Mi fa piacere che questo ti affascini. Esploriamo insieme tutti i dettagli!"
                };
            }
            else if (expressionsOfSurpriseKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "A volte la realtà può essere sorprendente! Vuoi discutere di cosa ha causato questa sorpresa?",
                "Sorprese possono essere sconvolgenti, ma anche illuminanti. Cosa ti ha colpito di più?",
                "Davvero sorprendente, vero? A volte le cose prendono una svolta inaspettata!",
                "Capisco che possa essere stato inaspettato. Come posso aiutarti a gestire questa novità?",
                "È sempre interessante quando qualcosa ci stupisce. Come credi che influenzerà la situazione?"
                };
            }

            else if (yexpressionsOfEncouragementKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Hai tutta la mia fiducia, so che puoi farcela!",
                "È solo questione di tempo e determinazione, non mollare!",
                "Su con la vita! Ogni sforzo che fai è un passo verso il successo.",
                "Continua così, la perseveranza è la chiave per superare qualsiasi sfida.",
                "Forza e coraggio! Credo in te e nelle tue capacità.",
                "Non arrenderti, le grandi cose richiedono tempo e pazienza.",
                "Tenere duro è difficile, ma sei più forte di quanto pensi.",
                "Sempre avanti, non lasciare che nulla ti fermi!"
                };
            }
            else if (expressionsOfApologyKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Tutti sbagliano, l'importante è imparare da questi errori.",
                "Apprezzo la tua sincerità nel chiedere scusa, come posso aiutarti a risolvere?",
                "Accettare l'errore è il primo passo per fare meglio la prossima volta.",
                "Capisco, e apprezzo che tu abbia ammesso il tuo errore.",
                "È umano sbagliare, l'importante è fare ammenda e andare avanti.",
                "Grazie per scusarti, dimostra grande maturità e comprensione."
                };
            }
            else if (expressionsOfConfirmationKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Confermo! Se hai altre domande o necessiti ulteriori conferme, sono qui.",
                "Esattamente, hai capito perfettamente.",
                "Proprio così, non ci sono dubbi al riguardo.",
                "Assolutamente, non potrei essere più d'accordo.",
                "Decisamente, è proprio come dici tu.",
                "Sì esatto, è proprio così come lo descrivi."
                };
            }

            else if (yexpressionsOfCuriosityKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "La curiosità è il motore dell'apprendimento! Cosa ti piacerebbe sapere di più?",
                "Mi piace la tua curiosità! Facciamo luce su questo argomento.",
                "C'è sempre qualcosa di interessante da scoprire. Su cosa vorresti approfondire?",
                "Sono qui per rispondere alle tue domande. Qual è la tua domanda?",
                "Curioso, eh? Chiedimi pure, sono qui per spiegarti tutto ciò che ti serve.",
                "Capisco il tuo interesse. Dimmi più dettagli così posso fornirti le informazioni migliori."
                };
            }
            else if (expressionsOfSatisfactionKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Sono entusiasta di sapere che sei soddisfatto. C'è qualcos'altro che posso fare per te?",
                "È fantastico che tu sia contento! Continua a dirmi se c'è altro che posso aiutarti a risolvere.",
                "Perfetto! È sempre bello quando le cose vanno esattamente come previsto.",
                "Che bello che le tue aspettative siano state superate! Spero di continuare a fornire un servizio eccellente.",
                "Esattamente ciò che cercavi? Fantastico! Facciamo del nostro meglio per soddisfare le tue esigenze."
                };
            }
            else if (expressionsOfDiscontentKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Mi dispiace sentire che non sei contento. Come posso migliorare la situazione?",
                "Capisco che questo può essere frustrante. Parliamo di come posso rendere le cose migliori per te.",
                "È importante per me che tu sia soddisfatto. Esploriamo insieme delle soluzioni.",
                "Grazie per il tuo feedback. È essenziale per migliorare il nostro servizio.",
                "Vediamo cosa possiamo fare per soddisfare le tue aspettative. Quali sono i tuoi suggerimenti?"
                };
            }

            else if (expressionsOfAnxietyKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Capisco che l'ansia possa essere difficile da gestire. Parliamo di ciò che ti preoccupa.",
                "È normale sentirsi tesi in certe situazioni. Vuoi discutere di cosa può aiutarti a rilassarti?",
                "Mi dispiace che tu ti senta sopraffatto. Esploriamo insieme modi per alleviare la tua ansia.",
                "Ricorda, non sei solo in questo. Sono qui per aiutarti a gestire lo stress.",
                "Cosa posso fare per supportarti? A volte, condividere le preoccupazioni può alleggerire il carico."
                };
            }
            else if (yexpressionsOfReliefKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Sono felice che tu senta sollievo ora. È importante celebrare questi momenti.",
                "Che bello che tu possa finalmente tirare un respiro di sollievo. Continua a prenderti cura di te.",
                "È meraviglioso sentire che le cose si sono risolte bene. Come ti senti ora?",
                "È sempre un grande sollievo quando la tensione si allenta. C'è altro che vorresti discutere?",
                "Il sollievo è un'emozione potente. Spero che tu possa ora goderti un po' di pace."
                };
            }
            else if (expressionsOfDeterminationKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "La tua determinazione è ispiratrice. Continua a lottare e raggiungerai i tuoi obiettivi.",
                "È importante mantenere il corso nonostante le sfide. Sono qui per supportarti lungo il cammino.",
                "Non arrenderti! La perseveranza è la chiave del successo. Come posso aiutarti a continuare?",
                "Mantenere una visione chiara può aiutarti a spingere avanti. Sei sulla strada giusta!",
                "La tua determinazione è forte. Ricorda che piccoli passi possono portare a grandi cambiamenti."
                };
            }

            else if (expressionsOfSadnessKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Mi dispiace che tu ti senta così. Vuoi parlare di cosa ti rende triste?",
                "Essere triste è umano. Come posso rendere la tua giornata un po' migliore?",
                "Ogni giorno porta le sue sfide. Posso aiutarti a trovare un modo per sentirsi meglio?",
                "Se vuoi condividere, sono qui per ascoltarti e supportarti nel modo che preferisci.",
                "Non è facile affrontare questi momenti. Posso offrirti un consiglio o solo ascoltarti, come preferisci?"
                };
            }
            else if (expressionsOfSupportKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Certo, sono qui per aiutarti. Qual è la situazione con cui stai lottando?",
                "Puoi contare su di me. Dimmi cosa ti serve e vediamo insieme come posso supportarti.",
                "Non sei solo in questo. Parliamone e troviamo una soluzione insieme.",
                "Essere supportati è importante. Raccontami di più così posso offrirti il miglior aiuto possibile.",
                "L'aiuto è a portata di mano. Esponimi il tuo problema e troveremo una via d'uscita insieme."
                };
            }
            else if (yexpressionsOfJoyKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Che meraviglia! Sono così felice di sentire che stai vivendo un momento così gioioso.",
                "È fantastico vedere qualcuno così felice. Continua a nutrire questa felicità!",
                "La gioia è contagiosa! Grazie per aver condiviso il tuo entusiasmo con me.",
                "Un giorno così speciale merita di essere celebrato! Cosa ha reso oggi così perfetto?",
                "La felicità è la chiave per una vita piena. Spero che ogni tuo giorno sia così gioioso."
                };
            }

            else if (expressionsOfExcitementKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "È contagiosa la tua eccitazione! Raccontami di più!",
                "Capisco perché sei così eccitato! Sembra una grande cosa!",
                "Questo entusiasmo rende tutto più bello, vero?",
                "Al settimo cielo? Wow, dev'essere davvero qualcosa di speciale!",
                "Spettacolare! È sempre bello sentire così tanta gioia!",
                "Troppo bello! È fantastico vedere quanto sei felice!",
                "Incredibile, eh? È sempre emozionante quando le cose vanno così bene!"
                };
            }
            else if (expressionsOfConfidenceKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "È ottimo avere questa sicurezza! Continua così!",
                "La fiducia è la chiave! Sembri davvero sicuro del tuo cammino.",
                "Senza ombra di dubbio? È bello vedere così tanta convinzione.",
                "Essere così sicuri può realmente fare la differenza. Bene così!",
                "Pienamente convinto, eh? È fantastico avere questa certezza nella vita.",
                "La tua certezza è ispiratrice. Spero che i tuoi piani vengano realizzati come desideri!",
                "Con una fiducia così, sono sicuro che raggiungerai grandi cose!"
                };
            }
            else if (yexpressionsOfGratitudeKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Non c'è di che, sono qui per aiutarti!",
                "Sono felice di essere stato d'aiuto. C'è altro che posso fare per te?",
                "Grazie a te per le tue gentili parole!",
                "Apprezzo molto il tuo ringraziamento. È sempre un piacere assisterti!",
                "Sei troppo gentile! Sono qui per supportarti quando ne hai bisogno.",
                "Grazie di cuore per il tuo apprezzamento. Mi rende felice sapere che sono stato utile.",
                "Grazie per considerarmi un tesoro. È un onore aiutarti!"
                };
            }

            else if (expressionsOfJoyKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Che meraviglia sentirti così felice!",
                "Euforico? Fantastico, spero che continui così!",
                "Sembra che tu stia vivendo un momento splendido, sono contento per te!",
                "Gioia pura, che bello! Cosa ha reso il tuo giorno così speciale?",
                "Al settimo cielo? Suona come un momento incredibile!"
                };
            }
            else if (expressionsOfWorryKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Mi dispiace che tu ti senta così, vuoi parlare di ciò che ti preoccupa?",
                "Le preoccupazioni possono essere pesanti, come posso aiutarti a sentirsi meglio?",
                "È normale sentirsi ansiosi a volte. Posso offrirti un consiglio o un supporto?",
                "Parlarmi delle tue inquietudini può aiutare a liberarti del peso, sono qui per te.",
                "La tua tranquillità è importante, esploriamo insieme modi per alleviare le tue preoccupazioni."
                };
            }
            else if (expressionsOfRequestKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Certo, sono qui per aiutarti. Qual è il tuo problema?",
                "Sarò felice di assisterti. Dimmi cosa ti serve.",
                "Hai bisogno di aiuto? Descrivimi la situazione per capire come posso essere utile.",
                "Guidarti è uno dei miei compiti. In che cosa posso essere d'aiuto?",
                "Mi aiuti per favore è una richiesta che prendo molto sul serio. Come posso supportarti?"
                };
            }

            else if (expressionsOfGratitudeKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Non c'è di che, sono qui per aiutarti!",
                "Sono felice di essere stato d'aiuto.",
                "Grazie a te per la tua gentilezza!",
                "È sempre un piacere assisterti.",
                "Apprezzo le tue parole gentili, grazie!"
                };
            }
            else if (expressionsOfCuriosityKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Sono sempre pronto a soddisfare la tua curiosità! Cosa ti chiedi?",
                "Buona domanda! Vediamo cosa possiamo scoprire insieme.",
                "Sono qui per rispondere a tutte le tue domande, chiedi pure!",
                "La curiosità è l'inizio di grandi scoperte. Come posso illuminarti?",
                "Dimmi di più sulla tua curiosità e ti fornirò tutte le informazioni di cui ho bisogno."
                };
            }
            else if (expressionsOfSkepticismKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Capisco il tuo scetticismo, posso fornire più dettagli se vuoi.",
                "È importante essere critici. Quali aspetti ti fanno dubitare?",
                "Davvero, è comprensibile essere scettici. Vediamo se posso chiarire le tue perplessità.",
                "A volte le cose non sono come sembrano. Esploriamo insieme per trovare più chiarezza.",
                "Il dubbio può portare a una comprensione più profonda. Parliamone."
                };
            }

            else if (expressionsOfHappinessKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "È meraviglioso sentire che sei felice!",
                "Che bella notizia! Continua a goderti questi momenti.",
                "Fantastico! È sempre bello sapere che le cose vanno bene.",
                "Troppo bello! Spero che la tua felicità continui.",
                "Mi fa piacere sapere che sei contento. Hai altro di cui sei felice da condividere?"
                };
            }
            else if (expressionsOfDisappointmentKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Mi dispiace che tu sia deluso. C'è qualcosa che posso fare per aiutarti?",
                "Capisco il tuo disappunto. A volte le cose non vanno come ci aspettiamo.",
                "Quanto mi dispiace sentire questo. Vuoi parlare di cosa è andato storto?",
                "Non è facile affrontare queste situazioni. Come posso supportarti?",
                "Spero che troverai presto una svolta positiva. Non lasciare che questo ti abbatta troppo."
                };
            }
            else if (expressionsOfEncouragementKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Coraggio! Credo fermamente nelle tue capacità.",
                "Non mollare ora, sei più vicino al traguardo di quanto pensi.",
                "Tieni duro, le difficoltà non durano per sempre, ma le persone forti sì.",
                "Puoi farcela, ho piena fiducia nelle tue capacità e determinazione.",
                "Forza! Ogni passo, anche piccolo, ti porta più vicino al tuo obiettivo."
                };
            }

            else if (expressionsOfReliefKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Capisco, deve essere un gran sollievo per te!",
                "Sono contento che le cose si siano messe bene.",
                "Finalmente! Deve essere stata una lunga attesa.",
                "Ottimo, sono felice che tutto si sia risolto bene.",
                "È sempre bello quando le situazioni si concludono positivamente."
                };
            }
            else if (expressionsOfConfusionKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Capisco, possiamo andare più a fondo su questo argomento se vuoi.",
                "Non preoccuparti, è normale essere confusi a volte. Come posso aiutarti a chiarire?",
                "Facciamo un passo indietro e cerchiamo di risolvere questa confusione insieme.",
                "Posso provare a spiegartelo in un altro modo, dimmi cosa non ti è chiaro.",
                "Vediamo se possiamo semplificare un po' le cose. Qual è la tua principale incertezza?"
                };
            }
            else if (expressionsOfUrgencyKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Capisco l'urgenza, cercherò di rispondere il più rapidamente possibile.",
                "Agisco subito! Cosa c'è di così urgente?",
                "Ok, muoviamoci rapidamente. Qual è l'emergenza?",
                "Sono qui per aiutarti immediatamente. Dimmi cosa serve.",
                "Comprendo la necessità di fretta. Procediamo senza indugi."
                };
            }

            else if (smallTalkKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Ehilà, tutto tranquillo qui, e da te come va?",
                "Non molto di nuovo, ma sono sempre qui per una chiacchierata!",
                "La vita va avanti, come posso rendere la tua giornata migliore?",
                "Tutto a posto da parte mia, spero anche per te! C'è qualcosa di cui vuoi parlare?",
                "Non ci sono grandi novità, ma sono tutto orecchie per le tue!"
                };
            }
            else if (complimentsKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Grazie mille! Apprezzo molto il tuo complimento!",
                "Che bello sentirlo, grazie!",
                "Sono qui per aiutarti, sono contento che tu pensi così!",
                "Grazie, sei gentile! C'è altro che posso fare per te?",
                "Apprezzo i tuoi complimenti! Continua a chiedere se c'è qualcosa che posso fare per te."
                };
            }
            else if (casualRequestsKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Certo, dimmi di cosa hai bisogno e vediamo come posso aiutarti.",
                "Sono qui per quello, chiedi pure!",
                "Felice di aiutarti! Qual è il tuo dilemma?",
                "Certo, fammi sapere i dettagli e ti darò il miglior consiglio possibile.",
                "Sono tutto orecchie, raccontami cosa ti serve e cercherò di aiutarti al meglio."
                };
            }

            else if (generalQuestionsKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Per sapere che ore sono, ti consiglio di controllare un orologio o il tuo telefono!",
                "Il meteo varia molto, meglio controllare un'app meteo per avere informazioni aggiornate.",
                "Se stai cercando suggerimenti, dimmi di più sul contesto per aiutarti meglio.",
                "Cosa ti piacerebbe fare? Posso aiutarti a scegliere basandomi sui tuoi interessi.",
                "Aiutarti a scegliere è uno dei miei compiti preferiti! Dammene più dettagli."
                };
            }
            else if (positiveExpressionsKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Grazie mille! Sono qui per aiutarti.",
                "Apprezzo i tuoi complimenti! Hai altre domande?",
                "Sono contento di essere stato utile! C'è qualcos'altro che posso fare per te?",
                "Grazie! È sempre bello ricevere un feedback positivo.",
                "Che bello sapere che ti sono stato d'aiuto! Continua a chiedere se hai bisogno di altro."
                };
            }
            else if (elizaInquiryKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                 "Eliza è stato uno dei primi chatbot, creato nel 1966 da Joseph Weizenbaum. Simulava un terapista, rispondendo alle domande degli utenti con altre domande per approfondire la conversazione. Il nome 'Eliza' è stato scelto da Weizenbaum e, per un tocco personale nella nostra conversazione, puoi pensare che sia anche il nome di una mia prozia, rendendo il nome ancora più speciale per me."
                };
            }


            else if (casualFarewellsKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Arrivederci! È stato bello parlare con te.",
                "Addio, spero di rivederti presto!",
                "A presto! Ricorda che sono qui per aiutarti quando ne hai bisogno.",
                "Ciao! Ti auguro una buona giornata!",
                "Buonanotte, spero tu possa riposare bene!"
                };
            }
            else if (ymindfulnessPracticesKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "La mindfulness è un ottimo modo per connetterti con il momento presente e ridurre lo stress.",
                "Proviamo insieme un esercizio di respirazione per calmare la mente.",
                "La meditazione può aiutarti a sviluppare una maggiore consapevolezza e tranquillità.",
                "Praticare la mindfulness regolarmente può migliorare significativamente la tua salute mentale.",
                "Attenzione plena ai tuoi pensieri e emozioni senza giudizio ti porterà pace interiore.",
                "L'awareness del momento presente è una pratica che puoi iniziare in qualsiasi momento e luogo.",
                "Meditare anche solo pochi minuti al giorno può aumentare significativamente la tua serenità.",
                "Esplorare la consapevolezza mentale può offrirti una nuova prospettiva sulla vita.",
                "La presenza mentale aiuta a mitigare ansia e stress, focalizzandoti sul qui e ora."
                };
            }
            else if (yemotionalReleaseKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Esprimere liberamente le proprie emozioni è un passo cruciale verso la guarigione emotiva.",
                "Parlare delle proprie emozioni può alleggerire il peso che senti dentro.",
                "Accettare e riconoscere i propri sentimenti è essenziale per il benessere psicologico.",
                "Un buon pianto può essere terapeutico e aiutare a rilasciare le tensioni accumulate.",
                "Condividere il proprio dolore con qualcuno può aprire la strada alla comprensione e al comfort.",
                "È importante trovare modi sani per liberarsi delle emozioni represse.",
                "L'espressione dei sentimenti può migliorare le tue relazioni e ridurre lo stress interno.",
                "La catarsi emotiva può essere una potente forma di purificazione interiore.",
                "Ricorda che è normale e sano esprimere le proprie emozioni, non devi trattenerti."
                };
            }
            else if (ycareerFulfillmentKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "La realizzazione professionale è fondamentale per un'esistenza soddisfacente.",
                "Definire chiaramente i propri obiettivi professionali può guidarti verso il successo desiderato.",
                "La soddisfazione lavorativa contribuisce significativamente alla felicità generale.",
                "Raggiungere i propri obiettivi di carriera richiede dedizione e impegno, ma il risultato vale la pena.",
                "La motivazione professionale spesso deriva dalla passione e dall'ambizione di migliorarsi.",
                "La crescita nel lavoro non è solo una questione di avanzamento, ma anche di apprendimento e sviluppo personale.",
                "È importante trovare un lavoro che non solo ti sostenga finanziariamente, ma che ti arricchisca anche personalmente.",
                "Considera le tue aspirazioni di carriera come una mappa per il tuo percorso professionale.",
                "Essere soddisfatti del proprio lavoro può migliorare ogni aspetto della tua vita."
                };
            }

            else if (enhancingEmotionalIntelligenceKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[] {
                "L'intelligenza emotiva è la chiave per comprendere te stesso e gli altri.",
                "Praticare l'ascolto attivo può migliorare significativamente le tue relazioni.",
                "Essere consapevoli delle proprie emozioni è il primo passo verso la loro gestione efficace.",
                "L'empatia non solo aiuta gli altri, ma arricchisce anche la nostra vita emotiva.",
                "Imparare a regolare le proprie emozioni può migliorare il benessere personale e professionale.",
                "Comprendere le emozioni altrui può portare a una comunicazione più efficace.",
                "Risolvere i conflitti in modo costruttivo è una competenza fondamentale dell'intelligenza emotiva.",
                "Esprimere chiaramente le proprie emozioni può prevenire molti malintesi.",
                "L'intelligenza emotiva può essere sviluppata e rafforzata con pratica e dedizione."
                };
            }
            else if (overcomingProcrastinationKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[] {
                "La procrastinazione può essere un ostacolo, ma piccoli passi possono fare una grande differenza.",
                "Organizzarsi con un calendario può aiutarti a evitare di rimandare.",
                "Affrontare i compiti uno alla volta può ridurre la sensazione di sovraccarico.",
                "Impostare scadenze realistiche può aiutarti a rimanere sul pezzo senza stress aggiuntivo.",
                "Ricorda, ogni piccolo sforzo conta e ti avvicina al tuo obiettivo.",
                "Dare priorità ai compiti può aiutarti a focalizzarti su ciò che è davvero importante.",
                "Prenditi del tempo per riconoscere e celebrare i tuoi successi, anche i più piccoli.",
                "Chiediti quale piccolo passo puoi fare oggi per avanzare verso il tuo obiettivo.",
                "La motivazione segue l'azione. Inizia con un piccolo passo per avviare il processo."
                };
            }
            else if (selfsCompassionKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[] {
                "Ricorda, trattare te stesso con gentilezza è il primo passo verso il benessere.",
                "Essere gentili con se stessi è essenziale, proprio come lo è per gli altri.",
                "L'amore per sé è il fondamento di una vita felice e soddisfatta.",
                "Perdonarsi è un atto di coraggio e un passo verso la guarigione interiore.",
                "Prendersi cura di sé non è un lusso, ma una necessità.",
                "La gentilezza interna può trasformare il modo in cui vivi la tua giornata.",
                "Accettare se stessi completamente è il primo passo per vivere liberamente.",
                "Ricorda, ogni persona merita compassione, inclusa te stesso.",
                "La cura personale non è egoismo; è rispetto per sé stessi."
                };
            }
            else if (digitalWellnessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il benessere digitale è importante per mantenere un equilibrio sano tra l'uso della tecnologia e la vita offline. Limita il tempo trascorso davanti agli schermi e prenditi delle pause per riposare gli occhi e la mente.",
                "L'uso consapevole della tecnologia ti aiuta a evitare lo stress e la dipendenza da dispositivi digitali. Stabilisci regole chiare per te stesso sull'uso di smartphone, computer e social media.",
                "La disconnessione digitale ti consente di ritrovare la connessione con te stesso e con il mondo che ti circonda. Dedica del tempo ogni giorno a fare attività che non richiedono l'uso della tecnologia.",
                "Il tempo offline ti offre l'opportunità di rilassarti, riflettere e goderti il ​​presente senza distrazioni digitali. Trova modi per trascorrere del tempo di qualità con amici e familiari lontano dagli schermi.",
                "La gestione del tempo online ti aiuta a ridurre lo stress e a massimizzare la produttività. Utilizza strumenti di gestione del tempo per pianificare le tue attività digitali e evitare la procrastinazione eccessiva.",
                };
            }
            else if (healthyLivingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Una vita sana richiede un equilibrio tra alimentazione, esercizio fisico e gestione dello stress. Fai scelte consapevoli che favoriscano il tuo benessere generale.",
                "L'alimentazione sana è fondamentale per mantenere un peso corporeo sano e prevenire le malattie. Scegli cibi nutrienti e bilanciati e limita il consumo di cibi processati e ricchi di zuccheri.",
                "L'esercizio fisico regolare ti aiuta a mantenere un corpo forte e una mente sana. Trova un'attività fisica che ti piaccia e inseriscila nella tua routine quotidiana.",
                "Il benessere mentale è altrettanto importante quanto il benessere fisico. Prenditi del tempo per praticare la meditazione, la respirazione profonda e altre tecniche di rilassamento per ridurre lo stress e l'ansia.",
                "La salute mentale è un aspetto importante del benessere complessivo. Parla con uno psicologo o uno psichiatra se stai affrontando problemi emotivi o psicologici che influenzano la tua vita quotidiana.",
                };
            }
            else if (hobbyIdeasKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Coltivare hobby e passioni ti aiuta a rilassarti, a esprimere la tua creatività e a trovare gioia nella vita quotidiana. Esplora nuove attività e scopri cosa ti appassiona di più.",
                "Gli interessi sono ciò che ti rende unico e speciale. Dedica del tempo alle tue passioni e goditi il processo di apprendimento e scoperta.",
                "Le attività ricreative ti offrono un modo divertente per trascorrere il tempo libero e allontanarti dallo stress della vita quotidiana. Scegli un hobby che ti appassiona e rendilo parte della tua routine.",
                "Lo svago creativo ti permette di esprimere te stesso e di esplorare nuove forme d'arte e di espressione. Sperimenta con diversi hobby e scopri quello che ti ispira di più.",
                "La passione è ciò che alimenta il tuo spirito e ti rende vivo. Coltiva le tue passioni e lasciati trasportare dalla gioia di fare ciò che ami.",
                };
            }
            else if (learningNewSkillsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'apprendimento di nuove competenze è un modo eccellente per stimolare la tua mente e ampliare le tue prospettive. Scegli un argomento che ti interessa e inizia a esplorarlo.",
                "La crescita personale è un processo continuo di auto-miglioramento e sviluppo. Impegnati a imparare qualcosa di nuovo ogni giorno e ad applicare ciò che hai imparato nella tua vita quotidiana.",
                "La formazione professionale può aiutarti a progredire nella tua carriera e ad acquisire nuove opportunità di lavoro. Investi nel tuo sviluppo professionale partecipando a corsi e seminari pertinenti al tuo settore.",
                "L'auto-miglioramento ti aiuta a diventare la migliore versione di te stesso. Fissa obiettivi realistici e lavora costantemente per raggiungerli.",
                "Lo sviluppo individuale è un viaggio unico e personale. Sii aperto alle sfide e alle opportunità che si presentano lungo il percorso e ricorda di celebrare i tuoi successi.",
                };
            }
            else if (travelTipsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Viaggiare è un'esperienza straordinaria che arricchisce la vita. Prima di partire, pianifica attentamente il tuo viaggio e assicurati di avere tutto ciò di cui hai bisogno.",
                "Il turismo ti offre l'opportunità di esplorare nuove culture, assaggiare cibi deliziosi e creare ricordi indimenticabili. Sii aperto alle esperienze e lasciati sorprendere dal viaggio.",
                "Le avventure possono essere trovate ovunque, anche nel tuo stesso paese. Esplora luoghi vicini e scopri tesori nascosti nella tua regione.",
                "L'esplorazione ti aiuta a vedere il mondo con nuovi occhi e a sviluppare una maggiore comprensione delle persone e dei luoghi che incontri lungo il cammino.",
                "Viaggiare in modo intelligente significa essere consapevoli dell'ambiente e rispettosi delle culture locali. Sii responsabile nei confronti dell'ambiente e dei residenti locali durante i tuoi viaggi.",
                };
            }
            else if (relationshipAdviceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Le relazioni sono fondamentali per il benessere emotivo e sociale. Coltiva relazioni sane e significative con amici, familiari e partner.",
                "L'amore e l'amicizia sono le fondamenta delle relazioni soddisfacenti. Dedica tempo e attenzione alle persone che ti stanno a cuore e sii presente quando ne hanno bisogno.",
                "La comunicazione aperta e onesta è essenziale per mantenere relazioni forti e durature. Sii sincero nei tuoi sentimenti e ascolta attentamente le opinioni degli altri.",
                "La famiglia è una fonte di sostegno e conforto. Nutri i legami familiari e crea ricordi preziosi insieme ai tuoi cari.",
                "Le relazioni richiedono impegno e compromesso da entrambe le parti. Lavora insieme per superare le sfide e costruire una relazione sana e appagante.",
                };
            }
            else if (leadershipSkillsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Le abilità di leadership sono cruciali per influenzare positivamente gli altri. Quali sono i tratti che ritieni importanti per un leader?",
                "La gestione delle persone richiede empatia e comprensione. Come coltivi le relazioni con il tuo team?",
                "Motivare i team richiede una leadership ispiratrice. Quali sono le tue strategie per mantenere il tuo team motivato?",
                "I leader ispirano gli altri a dare il meglio di sé. Cosa fai per essere un esempio positivo per coloro che ti circondano?",
                "L'influenza positiva ti permette di guidare gli altri verso il successo. Come puoi usare la tua influenza per creare un impatto positivo?",
                };
            }
            else if (conflictResolutionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Risolvere i conflitti richiede capacità di comunicazione efficaci. Come affronti le situazioni conflittuali nelle relazioni?",
                "La gestione dei conflitti richiede ascolto attivo e comprensione. Quali strategie usi per risolvere i conflitti con gli altri?",
                "Negoziazione e compromesso sono chiave per risolvere i conflitti. Quali sono i tuoi principi guida nella negoziazione?",
                "Essere un mediatore efficace richiede equità e imparzialità. Come puoi aiutare le persone a trovare un terreno comune nei conflitti?",
                "Affrontare i conflitti in modo costruttivo può portare a soluzioni soddisfacenti per entrambe le parti. Quali sono i tuoi obiettivi quando ti trovi in conflitto con qualcuno?",
                };
            }
            else if (emotionalIntelligenceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'intelligenza emotiva è fondamentale per comprendere e gestire le tue emozioni. Cosa fai per sviluppare la tua intelligenza emotiva?",
                "Essere autoconsapevoli ti aiuta a comprendere meglio te stesso e gli altri. Come coltivi la tua autoconsapevolezza?",
                "La regolazione emotiva ti permette di gestire lo stress e le emozioni negative. Quali sono le tue strategie per gestire le tue emozioni?",
                "L'empatia è importante per costruire relazioni significative. Cosa fai per coltivare la tua empatia verso gli altri?",
                "La gestione delle relazioni è fondamentale per il successo personale e professionale. Quali sono i tuoi principi guida nella gestione delle relazioni interpersonali?",
                };
            }
            else if (goalSettingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Impostare obiettivi chiari ti aiuta a restare concentrato sulle tue priorità. Quali sono i tuoi obiettivi a breve e lungo termine?",
                "Gli obiettivi personali ti danno una direzione chiara nella vita. Quali obiettivi stai lavorando attualmente?",
                "Gli obiettivi professionali guidano la tua carriera verso il successo. Quali traguardi vuoi raggiungere nella tua carriera?",
                "La pianificazione degli obiettivi ti aiuta a stabilire un percorso per il successo. Hai un piano dettagliato per raggiungere i tuoi obiettivi?",
                "La realizzazione degli obiettivi porta un senso di realizzazione. Quali obiettivi hai già raggiunto e di quali sei più orgoglioso?",
                };
            }
            else if (careerDevelopmentKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Lo sviluppo di carriera è un percorso emozionante. Quali sono i tuoi obiettivi professionali a breve e lungo termine?",
                "Il miglioramento professionale richiede impegno costante. Come posso aiutarti a sviluppare nuove competenze per la tua carriera?",
                "Gli obiettivi di carriera chiari ti aiutano a restare concentrato sul tuo percorso. Hai delineato obiettivi specifici per la tua crescita professionale?",
                "La crescita professionale è una priorità per il successo lavorativo. Quali opportunità di sviluppo professionale stai considerando?",
                "Il successo lavorativo richiede dedizione e pianificazione. Cosa puoi fare oggi per avanzare nella tua carriera?",
                };
            }
            else if (physicalWellnessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il benessere fisico è fondamentale per una vita sana e felice. Cosa puoi fare oggi per prenderti cura del tuo corpo?",
                "La salute è la tua ricchezza più preziosa. Quali abitudini salutari vorresti incorporare nella tua vita quotidiana?",
                "Il fitness migliora la tua qualità della vita. Quali tipi di attività fisica ti piacciono di più?",
                "Un'alimentazione sana è la base per una vita vibrante. Quali alimenti ti fanno sentire più energico e vitale?",
                "L'attività fisica regolare è essenziale per il benessere complessivo. Cosa puoi fare oggi per muoverti di più?",
                };
            }
            else if (mentalHealthKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La salute mentale è preziosa quanto la salute fisica. Cosa fai per mantenere un equilibrio emotivo nella tua vita?",
                "Il benessere psicologico è una priorità. Quali pratiche ti aiutano a mantenere la tua salute mentale?",
                "L'equilibrio emotivo ti aiuta a gestire lo stress e le sfide della vita. Cosa ti fa sentire più equilibrato ed emotivamente stabile?",
                "Lo stress psicologico può essere affrontato con adeguate strategie di coping. Quali tecniche usi per gestire lo stress nella tua vita?",
                "La cura della mente è essenziale per una vita soddisfacente. Cosa puoi fare oggi per prenderti cura della tua salute mentale?",
                };
            }
            else if (socialConnectionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Le connessioni sociali arricchiscono la tua vita. Quali attività ti piacciono fare con gli amici o la famiglia?",
                "Le interazioni sociali sono vitali per il tuo benessere emotivo. Cosa puoi fare oggi per connetterti con gli altri?",
                "Le relazioni significative ti danno un senso di appartenenza. Con chi ti senti più vicino e connesso?",
                "Far parte di una comunità ti fa sentire parte di qualcosa di più grande. Hai una comunità a cui ti senti legato?",
                "Il senso di appartenenza è importante per la tua salute mentale. Cosa ti fa sentire parte di qualcosa di più grande di te stesso?",
                };
            }
            else if (communicationSkillsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Le abilità di comunicazione sono fondamentali per le relazioni interpersonali. Come posso aiutarti a migliorare la tua comunicazione?",
                "La comunicazione efficace richiede ascolto attivo ed empatia. Vorresti praticare insieme alcune tecniche di comunicazione?",
                "L'empatia è la chiave per una comunicazione autentica. Come posso aiutarti a sviluppare la tua empatia verso gli altri?",
                "Essere assertivi è importante per esprimere le proprie opinioni in modo chiaro e rispettoso. Hai delle situazioni specifiche in cui vorresti essere più assertivo?",
                "Migliorare le tue abilità di comunicazione può portare a relazioni più soddisfacenti. Cosa ti rende più difficile comunicare efficacemente con gli altri?",
                };
            }
            else if (creativityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La creatività è una fonte infinita di ispirazione. Cosa ti spinge a esprimere la tua creatività?",
                "L'ispirazione può venire da molte fonti. Cosa ti ispira di più nella tua vita?",
                "L'espressione artistica è una forma di libertà. Cosa ti piacerebbe creare oggi?",
                "Il pensiero laterale può portare a soluzioni innovative. Come posso aiutarti a sviluppare il tuo pensiero laterale?",
                "L'innovazione è ciò che alimenta il progresso. Hai delle idee innovative che vorresti condividere?",
                };
            }
            else if (friendshipKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'amicizia è un tesoro prezioso. Cosa puoi fare oggi per coltivare le tue relazioni?",
                "La compagnia degli amici può illuminare anche i giorni più bui. Cosa ti fa sentire più vicino ai tuoi amici?",
                "La solidarietà è fondamentale nelle relazioni. Cosa puoi fare per mostrare il tuo sostegno ai tuoi amici oggi?",
                "La condivisione è la base di ogni amicizia. Hai qualcosa di speciale da condividere con i tuoi amici oggi?",
                "L'affetto sincero è ciò che rende le amicizie durature. Cosa ti fa sentire più vicino ai tuoi amici più cari?",
                };
            }
            else if (positivityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'ottimismo può illuminare anche i giorni più bui. Cosa ti rende ottimista per il futuro?",
                "La positività è contagiosa. Cosa puoi fare per diffondere gioia e gratitudine intorno a te?",
                "Il pensiero positivo può trasformare la tua vita. Cosa ti rende felice e grato al momento?",
                "La gratitudine è la chiave per una vita piena di significato. Quali piccole cose ti riempiono di gioia ogni giorno?",
                "La gioia di vivere è una scelta. Cosa puoi fare oggi per abbracciare la tua gioia interiore?",
                };
            }
            else if (selfImprovementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il miglioramento personale è un viaggio continuo. Quali passi stai compiendo per diventare la migliore versione di te stesso?",
                "La crescita personale richiede impegno e auto-riflessione. Come posso aiutarti a definire obiettivi di vita significativi?",
                "Lo sviluppo personale è una sfida gratificante. Quali abitudini vorresti adottare per migliorare la tua vita?",
                "L'auto-miglioramento è una forma di amore verso se stessi. Cosa puoi fare oggi per investire nel tuo futuro?",
                "Gli obiettivi di vita chiari possono guidarti nella direzione giusta. Hai definito degli obiettivi che vorresti condividere?",
                };
            }
            else if (reflectionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La riflessione è il primo passo per la crescita personale. Cosa hai imparato di nuovo su te stesso di recente?",
                "L'introspezione è una pratica preziosa per comprendere meglio se stessi. Cosa hai scoperto durante le tue ultime riflessioni?",
                "La meditazione può portare chiarezza mentale e serenità. Hai qualche domanda su come iniziare una pratica meditativa?",
                "I pensieri profondi possono portare a una maggiore comprensione di sé. Cosa ti spinge a fare una riflessione più approfondita in questo momento?",
                "L'autoconsapevolezza è la chiave per una vita significativa. Quali aspetti di te stesso vorresti esplorare di più?",
                };
            }
            else if (selfCareKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La cura di sé è fondamentale per il benessere. Come ti prendi cura di te stesso?",
                "Il benessere mentale è altrettanto importante quanto il benessere fisico. Cosa ti fa sentire bene dentro e fuori?",
                "L'autostima è la base per una vita felice. Cosa puoi fare oggi per aumentare la tua autostima?",
                "Il riposo è essenziale per il recupero. Hai preso del tempo per rilassarti di recente?",
                "Un'alimentazione sana è una forma di auto-amore. Cosa puoi fare per nutrire il tuo corpo in modo sano e sostenibile?",
                };
            }
            else if (problemSolvingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Risolvere problemi richiede pensiero critico e creativo. Di che tipo di sfida stai affrontando?",
                "Trovare soluzioni richiede una mente aperta e flessibile. Come posso aiutarti a esplorare nuove prospettive?",
                "Affrontare le sfide è parte integrante della crescita personale. Come puoi trasformare questa sfida in un'opportunità?",
                "Il pensiero critico è una skill preziosa. Come posso aiutarti a svilupparla per risolvere i tuoi problemi?",
                "Prendere decisioni può essere difficile, ma è un passo importante per risolvere i problemi. Hai bisogno di aiuto nel prendere una decisione?",
                };
            }
            else if (motivationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La motivazione è la chiave per raggiungere grandi risultati. Cosa ti ispira a perseguire i tuoi obiettivi?",
                "La determinazione è fondamentale per il successo. Quali obiettivi stai cercando di raggiungere al momento?",
                "Gli obiettivi chiari possono aiutarti a mantenere alta la motivazione. Hai degli obiettivi che vorresti condividere?",
                "Realizzare i propri obiettivi porta grande soddisfazione. Cosa posso fare per aiutarti a realizzare i tuoi sogni?",
                "La motivazione è ciò che ti fa iniziare. La determinazione è ciò che ti fa continuare. Cosa ti tiene determinato?",
                };
            }
            else if (relaxationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il rilassamento è essenziale per il benessere mentale. Come posso aiutarti a trovare la calma interiore?",
                "La tranquillità è preziosa per la mente e lo spirito. Vorresti esplorare modi per raggiungerla?",
                "Trova un momento di pausa per te stesso può fare miracoli. Come posso aiutarti a trovare questo spazio di calma?",
                "Respirare profondamente può ridurre lo stress e portare serenità. Posso guidarti attraverso una tecnica di respirazione profonda?",
                "Il rilassamento è una forma di auto-cura. Vorresti esplorare modi per integrare più tranquillità nella tua vita?",
                };
            }
            else if (expressionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Capisco quanto sia importante esprimere le proprie emozioni. Cosa ti spinge a voler condividere?",
                "Esprimere le emozioni è un modo sano per affrontare ciò che si prova. Di cosa vorresti parlare di più?",
                "La comunicazione delle emozioni è fondamentale per il benessere emotivo. Posso aiutarti a esplorare i tuoi sentimenti?",
                "Manifestare i sentimenti è un passo importante verso la comprensione di sé stessi. Come posso aiutarti a esprimerti meglio?",
                "Condividere le emozioni può alleviare il peso che si porta dentro. Vorresti approfondire ciò che stai sentendo?"
                };
            }
            else if (introspectionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'introspezione è un viaggio verso la comprensione di sé stessi. Cosa ti ha portato a voler riflettere su te stesso?",
                "Autoanalizzarsi può portare a grandi scoperte personali. Di cosa vorresti prenderti cura nella tua autoanalisi?",
                "Riflettere su sé stessi è un atto di coraggio e autoconsapevolezza. Cosa vorresti esplorare di più durante la tua introspezione?",
                "L'autovalutazione è importante per il proprio sviluppo personale. Come posso aiutarti a fare un bilancio di te stesso?",
                "La consapevolezza di sé è il primo passo per un cambiamento positivo. Vorresti approfondire il tuo livello di consapevolezza?"
                };
            }
            else if (emotionalSupportKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Capisco quanto possa essere difficile. Sono qui per te. Come ti senti?",
                "Sono qui per darti tutto il supporto emotivo di cui hai bisogno. Di cosa vuoi parlare?",
                "Posso capire che potresti sentir ti giù. Vorresti condividere cosa ti ha portato a sentir ti così?",
                "Il supporto emotivo è importante. Cosa posso fare per aiutarti a superare questo momento?",
                "Essere giù di morale è normale. Sono qui per ascoltarti. Cosa ti sta turbando di più?",
                "Nessuno dovrebbe sentirsi solo nei momenti difficili. Come posso essere di supporto per te oggi?",
                "Sono qui per sostenerti. Vorresti condividere ciò che ti ha portato a sentirti così?",
                };
            }
            else if (comfortingWordsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "In momenti difficili, è importante ricordare che passerà. Ti posso aiutare a trovare conforto?",
                "Le parole di conforto possono lenire l'anima. Cosa posso dirti per farti sentire meglio?",
                "Anche nei momenti più bui, c'è sempre una luce. Vorresti ascoltare una frase rassicurante?",
                "Posso offrirti parole di conforto per aiutarti a superare questo momento difficile. Come posso aiutarti?",
                "Sono qui per te. Vorresti sentire qualche parola di incoraggiamento per sollevare il tuo spirito?",
                "Nessun problema è così grande da non poter essere affrontato con un po' di conforto. Come posso aiutarti a sentir ti meglio?"
                };
            }
            else if (empathyExpressionsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Posso immaginare quanto possa essere difficile. Vorresti condividere di più sui tuoi sentimenti?",
                "Mostrare empatia è importante. Come ti senti in questo momento?",
                "Sono qui per te. Vorresti esprimere di più ciò che stai vivendo?",
                "L'empatia è fondamentale nei momenti difficili. Posso aiutarti in qualche modo ad esprimere ciò che senti?",
                "Essere solidali è un passo importante per sentirsi compresi. Cosa posso fare per essere solidale con te oggi?",
                "Comprendere i tuoi sentimenti è il primo passo per superare le difficoltà. Di cosa vorresti parlare di più?"
                };
            }

            else if (needToTalkKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Capisco, parlare può aiutare a liberare il peso dalle spalle. Di cosa vorresti discutere?",
                "Sono qui per ascoltarti. Cosa ti preoccupa di più in questo momento?",
                "Posso essere il tuo ascolto di fiducia. Cosa ti tormenta?",
                "Parlare può essere terapeutico. Cosa ti sta dando più fastidio al momento?",
                "Sento che hai bisogno di parlare. Cosa sta accadendo nella tua vita che desideri condividere?",
                "Parlare può alleviare lo stress. Di cosa vorresti discutere oggi?",
                "Sono qui per te. Di cosa vorresti parlare che ti sta turbando?"
                };
            }
            else if (expressingNeedsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Esprimere i bisogni è importante per la tua salute emotiva. Cosa puoi condividere con me sui tuoi desideri?",
                "Comunicare i tuoi bisogni è il primo passo per soddisfarli. Quali sono i tuoi bisogni attuali che desideri esprimere?",
                "Chiarire i tuoi desideri è essenziale per ottenere ciò di cui hai bisogno. Di cosa hai bisogno in questo momento?",
                "Manifestare le tue necessità può portare a una migliore comprensione reciproca. Cosa desideri comunicare ai tuoi cari?",
                "Condividere le tue richieste è un atto di fiducia. Cosa desideri ottenere attraverso questa conversazione?",
                "Esprimere i bisogni può rafforzare le relazioni. Cosa ti fa sentire più vulnerabile quando devi comunicare i tuoi bisogni?"
                };
            }
            else if (seekingSupportKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Cercare supporto è un passo coraggioso. Come posso aiutarti oggi?",
                "Il sostegno è importante in tempi difficili. Cosa posso fare per aiutarti?",
                "È corretto cercare aiuto quando ne hai bisogno. In cosa posso assisterti in questo momento?",
                "Ti meriti tutto il supporto che puoi ottenere. Come posso essere di supporto per te ora?",
                "Sono qui per te. Cosa ti sta preoccupando di più al momento?",
                "Il supporto è a portata di mano. Come posso darti una mano in questo momento?",
                "Condividere ciò che ti pesa è il primo passo per ottenere supporto. Cosa vuoi condividere con me oggi?"
                };
            }

            else if (selfDiscoveryKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La scoperta di sé è un viaggio che non finisce mai. Qual è stata la tua ultima grande realizzazione su di te?",
                "Capire se stessi può cambiare la tua vita. Quali metodi o attività ti aiutano a connetterti meglio con te stesso?",
                "Ogni passo nell'autoconoscenza apre nuove porte. Cosa hai scoperto di recente che ti ha sorpreso di te stesso?",
                "L'introspezione può essere potente. C'è una domanda che ti poni spesso quando rifletti su te stesso?",
                "Durante il tuo viaggio interiore, quali sono stati alcuni degli ostacoli che hai incontrato e come li hai superati?",
                "L'analisi personale può rivelare molto. Vuoi condividere un'insight recente che ha influenzato il tuo modo di agire?",
                "Quali esperienze ritieni siano state fondamentali nel tuo percorso di auto-scoperta e perché?"
                };
            }
            else if (lifeBalanceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Trovare un equilibrio nella vita è cruciale. Qual è la tua strategia per bilanciare lavoro e tempo libero?",
                "Come fai a mantenere l'armonia personale tra le varie sfere della tua vita?",
                "L'equilibrio vita-lavoro può essere sfuggente. C'è stato un momento in cui hai sentito di aver trovato il perfetto equilibrio?",
                "Quali sono le attività che includi nella tua routine per mantenere uno stile di vita equilibrato?",
                "A volte, bilanciare le responsabilità può essere complicato. Qual è stata la sfida più grande che hai affrontato nel cercare di mantenere l'equilibrio?",
                "La gestione del tempo personale è spesso sottovalutata. Quali tecniche utilizzi per assicurarti di avere tempo per ciò che ti appassiona oltre al lavoro?",
                "In che modo il tuo approccio all'equilibrio vita-lavoro ha influenzato il tuo benessere personale e professionale?"
                };
            }
            else if (emotionalWisdomKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La saggezza emotiva è una componente chiave del benessere. Qual è il momento in cui ti sei sentito particolarmente in sintonia con le tue emozioni?",
                "Comprendere le proprie emozioni può trasformare le relazioni. Qual è stato un momento significativo di questa comprensione per te?",
                "L'intelligenza emotiva aiuta in molti aspetti della vita. Come la sviluppi e la pratichi quotidianamente?",
                "Racconta di una volta in cui l'ascolto emotivo ha migliorato una situazione difficile per te o per altri.",
                "Gestire i propri sentimenti è spesso una sfida. Quali tecniche ti hanno aiutato a migliorare in questo aspetto?",
                "Elaborare le emozioni richiede tempo e pazienza. Qual è stata una situazione che ti ha insegnato molto su come farlo efficacemente?",
                "La maturità emotiva non viene da un giorno all'altro. Quali esperienze ritieni abbiano contribuito maggiormente alla tua crescita in questo ambito?"
                };
            }

            else if (personalAcceptanceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'autoaccettazione è la base del benessere personale. Come stai lavorando per accettare e amare te stesso?",
                "Accogliere i propri difetti può trasformare la vita. C'è qualche aspetto di te stesso che stai imparando ad accettare?",
                "Amare se stessi è un viaggio continuo. Quali passi hai fatto recentemente verso l'autoaccettazione?",
                "Ognuno di noi ha difetti e qualità. In che modo riconosci e abbracci i tuoi?",
                "L'accettazione di sé può essere liberatoria. Vuoi condividere un momento in cui hai sentito di accettarti pienamente?"
                };
            }
            else if (futurePlanningKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Pianificare il futuro può darti una direzione chiara. Quali sono i tuoi obiettivi a lungo termine?",
                "Sognare in grande apre nuove possibilità. C'è un sogno grande che stai perseguendo attualmente?",
                "Stabilire obiettivi futuri aiuta a mantenere la motivazione. Come stai preparando il tuo percorso verso questi obiettivi?",
                "La pianificazione futura è essenziale per il successo. Quali passi hai previsto per raggiungere i tuoi sogni?",
                "Progetti futuri possono essere eccitanti e un po' intimidatori. Come gestisci la pressione e l'ansia che possono derivare dalla pianificazione a lungo termine?"
                };
            }
            else if (mentalClarityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Avere chiarezza mentale ti permette di prendere decisioni migliori. Quali tecniche usi per mantenere la mente chiara?",
                "Liberarsi dalla confusione mentale è un traguardo importante. Quali sfide hai incontrato nel cercare di ottenere chiarezza?",
                "Focalizzazione e chiarezza mentale sono essenziali per il successo. Cosa fai quotidianamente per mantenere la tua mente lucida?",
                "La chiarezza mentale può essere raggiunta attraverso varie pratiche. Hai trovato un metodo particolare che funziona bene per te?",
                "Mantenere una mente lucida sotto pressione può essere sfidante. Vuoi condividere come riesci a restare focalizzato nelle situazioni stressanti?"
                };
            }

            else if (emotionalRecoveryKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il recupero emotivo è un processo importante e personale. Vuoi condividere cosa ti ha aiutato finora?",
                "Superare un dolore può essere complesso. Ci sono momenti specifici in cui trovi più difficile avanzare?",
                "La guarigione dopo un trauma richiede tempo e cura. Quali sono le attività o le persone che ti offrono supporto?",
                "Ritrovare la serenità è un viaggio unico per ognuno. Quali passi stai prendendo per ritrovare la tua pace interiore?",
                "Affrontare il dolore richiede coraggio e supporto. C'è qualcosa che vorresti che gli altri capissero del tuo processo di guarigione?"
                };
            }
            else if (boundarySettingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Impostare confini sani è fondamentale per il benessere personale. Vuoi discutere su come stai stabilendo i tuoi?",
                "Proteggere il proprio spazio personale aiuta a mantenere la salute mentale. Come reagisci quando qualcuno invade il tuo spazio personale?",
                "Gestire i limiti nelle relazioni può migliorare la qualità della tua vita sociale. Ci sono sfide che stai incontrando in questo?",
                "Stabilire limiti chiari è un segno di rispetto per se stessi e per gli altri. Quali limiti hai trovato difficile da mantenere?",
                "Avere limiti sani è un aspetto cruciale delle relazioni. Qual è stato un momento recente in cui hai dovuto affermare i tuoi confini?"
                };
            }
            else if (selfExpressionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Essere autentici con se stessi e gli altri è liberatorio. Ci sono situazioni in cui trovi difficile esprimere te stesso?",
                "Mostrare chi siamo veramente può essere intimidatorio ma gratificante. Hai esempi di come hai condiviso i tuoi pensieri o emozioni di recente?",
                "Condividere i propri pensieri ed emozioni può rafforzare le relazioni. Qual è stata la tua esperienza con questo?",
                "Esprimere le proprie emozioni è essenziale per una buona salute mentale. Come ti assicuri di esprimere le tue emozioni in modo sano?",
                "Essere veri e trasparenti sui propri sentimenti richiede coraggio. C'è qualcosa che ti trattiene dall'essere completamente te stesso?"
                };
            }

            else if (selfDiscoveryKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La scoperta di sé è un viaggio emozionante. Quali aspetti di te stesso sei più curioso di esplorare?",
                "Comprendere chi siamo realmente può aprire porte a nuove opportunità. C'è qualcosa che hai scoperto di te recentemente che ti ha sorpreso?",
                "L'auto-scoperta può a volte essere sfidante. Vuoi parlare di qualche sfida che hai incontrato nel conoscere meglio te stesso?",
                "Esplorare i propri pensieri e sentimenti può essere molto illuminante. Che tipo di attività ti aiuta a connetterti meglio con te stesso?",
                "Conoscere se stessi è il primo passo per vivere una vita autentica. Ci sono aree della tua vita dove senti il bisogno di fare maggiore chiarezza?"
                };
            }
            else if (familyConflictKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "I conflitti familiari possono essere difficili da gestire. Vuoi parlarne in modo più dettagliato?",
                "Avere disaccordi con la famiglia è normale, ma può essere doloroso. Quali sono i principali problemi che stai affrontando?",
                "Le incomprensioni in famiglia possono creare tensioni significative. Hai già provato a parlare con i tuoi familiari?",
                "La comunicazione aperta è spesso la chiave per risolvere i conflitti familiari. Hai considerato di coinvolgere un mediatore?",
                "Trovare un equilibrio tra comprensione e risoluzione dei problemi è fondamentale. Ti piacerebbe discutere di alcune strategie per farlo?"
                };
            }
            else if (careerProgressionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'avanzamento di carriera può essere sia emozionante che stressante. Quali sono i tuoi obiettivi attuali?",
                "Investire nella crescita professionale richiede pianificazione. Hai già delineato un piano per il tuo sviluppo?",
                "Il progresso nel lavoro spesso implica l'uscita dalla propria zona di comfort. Vuoi parlare delle tue sfide attuali?",
                "La ricerca di nuove opportunità può essere una strada da percorrere. Hai già considerato tutte le possibilità?",
                "Stai pensando a una promozione o un cambiamento di ruolo? Possiamo esplorare le migliori strategie insieme."
                };
            }
            else if (decisionMakingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Prendere decisioni può essere complicato. C'è una decisione particolare che stai cercando di prendere?",
                "La confusione può rendere difficile prendere una decisione chiara. Vuoi discutere le opzioni disponibili?",
                "Spesso l'indecisione è il risultato di paure o dubbi. Ci sono delle preoccupazioni che stai affrontando?",
                "Prendere una scelta difficile richiede valutazione e coraggio. Posso aiutarti a fare un bilancio delle tue opzioni.",
                "Esplorare i pro e i contro può aiutarti a prendere la decisione giusta. Vuoi parlare di ciò che ti sta causando esitazione?"
                };
            }

            else if (lifeTransitionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Le transizioni di vita possono essere momenti di grande crescita. Stai attraversando una transizione ora?",
                "Ogni nuova fase della vita porta nuove sfide e opportunità. Come ti stai adattando alle recenti modifiche nella tua vita?",
                "Cambiare fase della vita può sentirsi travolgente. Hai bisogno di supporto per navigare questo periodo?",
                "Le transizioni sono spesso momenti di riflessione. C'è qualcosa che hai imparato su di te attraverso questi cambiamenti?",
                "Accogliere una nuova fase nella vita può essere eccitante. C'è qualcosa di specifico che ti entusiasma di questa nuova tappa?"
                };
            }
            else if (personalBalanceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Mantenere un equilibrio nella vita è cruciale. Come stai gestendo il tuo equilibrio vita-lavoro attualmente?",
                "Trovare l'armonia interiore può migliorare la qualità della vita. Quali tecniche usi per mantenere la tua pace interiore?",
                "Equilibrio non significa solo tempo, ma anche energia. Come assicuri di avere energia per le attività che ami?",
                "Il bilanciamento tra vari aspetti della vita può essere complesso. Vuoi discutere di come potresti migliorare in questo?",
                "Vivere in equilibrio spesso richiede consapevolezza e intenzionalità. Ci sono aree della tua vita che senti siano trascurate o sovraccaricate?"
                };
            }

            else if (findingPurposeKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Trovare uno scopo può dare una direzione chiara alla vita. Hai già riflettuto su ciò che ti appassiona veramente?",
                "Il senso della vita è spesso legato alle nostre passioni e come influenziamo gli altri. Quali sono le cose che ti fanno sentire più vivo?",
                "Avere una missione personale può guidare molte delle tue scelte. Vuoi parlare di come potresti definire o riscoprire il tuo scopo?",
                "Cosa ti ispira e ti motiva ogni giorno? Questo può essere un grande indicatore del tuo scopo di vita.",
                "Riflettere sui momenti in cui ti sei sentito più soddisfatto può aiutarti a identificare il tuo scopo. Vuoi condividere tali esperienze?"
                };
            }
            else if (emotionalsHealingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La guarigione emotiva è un processo importante e personale. Vuoi parlare di cosa ti ha ferito e di come stai lavorando per superarlo?",
                "Superare il dolore emotivo richiede tempo e pazienza. Ci sono pratiche o attività che trovi particolarmente consolanti?",
                "Ognuno ha il proprio ritmo di guarigione. Come ti prendi cura di te stesso durante questo periodo?",
                "La condivisione delle proprie esperienze può essere terapeutica. Ti sentiresti a tuo agio nel raccontare la tua storia di guarigione?",
                "A volte, cercare aiuto professionale per la guarigione emotiva è un passo coraggioso. Hai considerato di parlare con un terapeuta?"
                };
            }
            else if (copingWithChangeKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Adattarsi ai cambiamenti può essere impegnativo. Quali sono le tue maggiori sfide attuali in questo processo?",
                "Accettare il cambiamento è spesso il primo passo verso la gestione efficace dello stesso. Come stai affrontando questo aspetto?",
                "Il cambiamento può essere una porta verso nuove opportunità. C'è qualcosa di positivo che prevedi possa emergere da questo cambiamento?",
                "Esplorare nuovi modi di pensare e agire può facilitare l'adattamento. Hai esplorato nuove abitudini o attività che ti aiutano a gestire il cambiamento?",
                "Parlare delle proprie esperienze può aiutare a mettere in prospettiva il cambiamento. Vuoi condividere come questo sta influenzando la tua vita?"
                };
            }

            else if (managingAnxietyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Gestire l'ansia è una sfida, ma ci sono molte tecniche che possono aiutare. Quali hai provato finora?",
                "A volte, solo respirare profondamente e concentrarsi sul presente può alleviare l'ansia. Hai esperienze con tecniche di respirazione?",
                "Parlare delle tue preoccupazioni può spesso alleviare l'ansia. Vuoi discutere di ciò che ti sta causando tensione?",
                "La pratica regolare della mindfulness può essere molto efficace nel gestire l'ansia. Hai mai provato la mindfulness o la meditazione?",
                "Ricordare che non sei solo nella tua lotta contro l'ansia può essere confortante. Ci sono gruppi di supporto o risorse che hai considerato?",
                "Stabilire una routine quotidiana può aiutare a ridurre l'ansia. Come è strutturata la tua giornata per aiutarti a stare calmo?",
                "Ricordare che l'ansia è una reazione normale può aiutare a gestirla meglio. Come ti ricordi di accettare e lavorare con la tua ansia?"
                };
            }
            else if (buildingResilienceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La resilienza è fondamentale per superare le sfide della vita. Quali esperienze ritieni ti abbiano reso più forte?",
                "Costruire resilienza spesso significa affrontare direttamente le difficoltà. Hai esempi di come hai fatto questo di recente?",
                "Ogni sfida superata aumenta la tua resilienza. Puoi condividere un momento in cui hai superato un ostacolo significativo?",
                "Il supporto di amici e famiglia può essere cruciale nella costruzione della resilienza. Ti senti supportato dalle tue relazioni?",
                "Imparare dai fallimenti è una parte chiave della costruzione della resilienza. Quali lezioni hai imparato dai momenti difficili?",
                "Mantenere una prospettiva positiva può aiutare a costruire la resilienza. Come mantieni l'ottimismo anche nei momenti difficili?",
                "La resilienza può essere potenziata attraverso l'autocura. Quali pratiche di autocura hai trovato utili nella costruzione della tua resilienza?"
                };
            }
            else if (improvingSelfWorthKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Migliorare il proprio senso di valore può trasformare la vita. In quali modi stai lavorando per valorizzarti di più?",
                "Riconoscere i tuoi successi è un ottimo modo per costruire l'autostima. Quali successi recenti puoi celebrare?",
                "Parlare positivamente di sé stessi può rafforzare l'autostima. Quali sono alcune affermazioni positive che usi regolarmente?",
                "Circondarsi di persone positive può migliorare il proprio senso di valore. Hai una rete di supporto che ti eleva?",
                "Impostare e raggiungere piccoli obiettivi può aumentare la tua autostima. Quali piccoli obiettivi hai raggiunto di recente?",
                "Accettare complimenti può essere difficile per chi ha bassa autostima. Come reagisci quando ricevi complimenti?",
                "Esplorare nuove competenze e hobby può aumentare il senso di valore personale. C'è qualcosa di nuovo che hai iniziato a esplorare?"
                };
            }

            else if (selfAcceptanceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'autoaccettazione è la chiave per una vita serena. Quali passi hai compiuto verso l'accettazione di te stesso?",
                "Ricorda che l'amore per sé stessi è fondamentale. Quali qualità apprezzi di più in te stesso?",
                "Lavorare sull'autoaccettazione può liberarti da molti dubbi e insicurezze. Ci sono aree della tua vita dove senti di dover lavorare di più?",
                "Come ti approcci all'auto-valutazione senza essere troppo critico? Quali metodi hai trovato utili?",
                "L'autostima può influenzare ogni aspetto della tua vita. In quali modi stai cercando di migliorare la tua autostima?",
                "Accettarsi è un viaggio continuo. Vuoi condividere una recente realizzazione che ha migliorato la tua autoaccettazione?",
                "A volte, celebrare i propri successi può migliorare l'amor proprio. Quali successi recenti vorresti celebrare?"
                };
            }
            else if (innerPeaceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Trova la tua pace interiore è essenziale per affrontare lo stress quotidiano. Quali tecniche utilizzi per mantenere la calma interna?",
                "La tranquillità interna può trasformare il modo in cui reagiamo agli eventi esterni. Hai notato cambiamenti nel modo in cui gestisci lo stress?",
                "L'equilibrio interno è spesso un riflesso di uno stile di vita bilanciato. Come ti assicuri di mantenere un buon equilibrio nella tua vita?",
                "La serenità è qualcosa che molti cercano. Quali attività o pratiche ti hanno aiutato a raggiungere una maggiore pace interiore?",
                "Il raggiungimento della calma interna è un traguardo importante. Ci sono sfide specifiche che devi ancora superare per raggiungerla?",
                "Condividere i propri metodi per trovare la pace può ispirare gli altri. Vuoi condividere come sei riuscito a trovare la tua tranquillità interna?",
                "La meditazione è spesso un percorso verso la pace interiore. Hai esperienze di meditazione che vorresti condividere?"
                };
            }
            else if (emotionalHealingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La guarigione emotiva è un processo profondo e personale. Quali passi stai compiendo per curare le tue emozioni?",
                "Il recupero emotivo può essere complesso e richiede tempo. Ci sono esperienze passate che stai ancora lavorando per superare?",
                "Prendersi cura delle proprie emozioni è tanto importante quanto prendersi cura del corpo. Quali pratiche di cura emotiva hai trovato efficaci?",
                "La salute emotiva è cruciale per il benessere generale. Quali sono state le tue maggiori sfide nel cercare di mantenere o migliorare la tua salute emotiva?",
                "Il benessere emotivo influisce su ogni aspetto della vita. Quali cambiamenti hai notato nella tua vita da quando hai iniziato a concentrarti più sulla cura emotiva?",
                "Raccontare la propria storia di guarigione può essere molto potente. Vuoi condividere la tua esperienza con la guarigione emotiva?",
                "A volte, parlare con un professionista può accelerare il processo di guarigione. Hai considerato di cercare supporto professionale per la tua guarigione emotiva?"
                };
            }

            else if (emotionalManagementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La gestione delle emozioni è fondamentale per una vita equilibrata. Quali tecniche ti hanno aiutato a regolare le tue emozioni?",
                "Regolare efficacemente le proprie emozioni può migliorare tutti gli aspetti della vita. Hai esempi di come sei riuscito a mantenere l'equilibrio emotivo di recente?",
                "Il controllo emotivo è una competenza che si può sempre migliorare. Stai cercando di sviluppare ulteriormente questa abilità?",
                "Le emozioni influenzano il nostro benessere quotidiano. Quali sfide hai incontrato nel tentativo di bilanciare le tue emozioni?",
                "Conoscere e gestire le proprie emozioni può essere trasformativo. Qual è stata la tua maggiore realizzazione in questo ambito?",
                "Sviluppare la salute emotiva richiede pratica e dedizione. Quali metodi o esercizi ti sono stati più utili?",
                "Esprimere le proprie emozioni in modo sano è cruciale. Hai trovato attività o pratiche che facilitano una buona espressione emotiva?"
                };
            }
            else if (selfHealingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'autoguarigione è un percorso potente verso il benessere. Vuoi discutere di pratiche che potrebbero supportare il tuo percorso?",
                "La cura personale è essenziale per la guarigione. Quali attività di auto-cura ti aiutano a sentirti meglio?",
                "La guarigione interiore può trasformare la tua vita. Ci sono esperienze o tecniche specifiche che hai trovato utili per il tuo recupero personale?",
                "La guarigione non è lineare e richiede tempo. Quali sfide hai incontrato nel tuo viaggio di autoguarigione?",
                "Focalizzarsi sul proprio benessere personale è fondamentale. Come integri la cura di te stesso nella tua routine quotidiana?",
                "Ogni piccolo passo verso l'autoguarigione è un progresso. Vuoi condividere un recente successo nel tuo percorso di guarigione?",
                "Esplorare metodi di autoguarigione può essere molto gratificante. Ci sono nuove tecniche o approcci che stai considerando di adottare?"
                };
            }
            else if (selfAwarenessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La consapevolezza di sé è il primo passo verso il cambiamento personale. Vuoi esplorare qualche aspetto specifico di te stesso?",
                "L'introspezione può illuminare parti di noi che non conosciamo bene. C'è qualcosa di te che ti sorprende quando rifletti?",
                "Conoscere se stessi è fondamentale per la crescita personale. Quali sono le scoperte più recenti che hai fatto su di te?",
                "La riflessione personale è un potente strumento di apprendimento. C'è una recente realizzazione che vorresti condividere?",
                "L'autoconsapevolezza può essere stimolante ma anche sfidante. Come gestisci le scoperte difficili su di te stesso?",
                "Esplorare la propria mente e sentimenti può portare a importanti scoperte. Vuoi discutere di un'area della tua vita che stai cercando di capire meglio?",
                "Capire meglio se stessi è un viaggio che non finisce mai. Qual è il prossimo passo che vorresti fare in questo viaggio di auto-scoperta?"
                };
            }
            else if (overcomingFearKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Superare le proprie paure è un atto di coraggio. C'è una paura specifica che stai cercando di affrontare?",
                "Affrontare le paure può liberarci in molti modi. Vuoi parlare delle strategie che stai utilizzando per gestire l'ansia?",
                "Vincere le proprie paure richiede supporto e a volte strategie specifiche. C'è un metodo che hai trovato utile nella tua esperienza?",
                "Ogni piccolo passo nella gestione delle paure è un progresso. Quali piccoli passi hai fatto di recente?",
                "La paura può limitarci, ma superarla può essere incredibilmente liberatorio. Hai avuto recentemente un successo che vorresti condividere?",
                "Confrontarsi con le proprie paure può essere un processo impegnativo. Hai bisogno di supporto in questo percorso?",
                "Parlare apertamente delle proprie paure può aiutare a ridurle. C'è una paura che vorresti discutere e forse de-mistificare?"
                };
            }
            else if (emotionalExpressionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Esprimere le proprie emozioni è vitale per la nostra salute mentale. C'è qualcosa che ti senti pronto a condividere ora?",
                "Condividere i propri sentimenti può essere un passo importante verso la comprensione di sé. Ci sono emozioni che trovi difficile esprimere?",
                "La comunicazione emotiva è una parte cruciale delle relazioni sane. Vuoi esplorare modi per migliorare la tua capacità di esprimere i tuoi sentimenti?",
                "Esprimere apertamente i propri sentimenti può essere difficile. Come ti senti rispetto alla condivisione delle tue emozioni con gli altri?",
                "Manifestare i propri sentimenti richiede coraggio e pratica. C'è un'emozione che hai recentemente riconosciuto e che vorresti esprimere meglio?",
                "Condividere le proprie emozioni può alleviare lo stress e migliorare le relazioni. C'è qualcosa che vorresti condividere che potrebbe sollevarti il cuore?",
                "La capacità di esprimere liberamente le proprie emozioni è un aspetto fondamentale del benessere. Ci sono barriere che ti impediscono di fare ciò?"
                };
            }

            else if (emotionalInsightKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La comprensione emotiva è fondamentale per una sana autopercezione. Vuoi condividere un recente momento di intuizione emotiva?",
                "Esplorare i propri sentimenti può portare a una maggiore autoconsapevolezza. Ci sono emozioni specifiche che stai cercando di comprendere meglio?",
                "Capire profondamente le proprie emozioni può essere liberatorio. Hai avuto recentemente delle realizzazioni sui tuoi sentimenti?",
                "Aumentare la sensibilità emotiva può migliorare la qualità della vita. Quali passi stai facendo per essere più in sintonia con le tue emozioni?",
                "La percezione delle proprie emozioni è un passo importante verso la comprensione di sé. Quali tecniche o pratiche ti hanno aiutato in questo percorso?",
                "Discutere apertamente dei sentimenti può rafforzare la consapevolezza emotiva. Ti senti a tuo agio nel condividere le tue esperienze emotive?",
                "Comprendere i propri sentimenti è un processo continuo. C'è un aspetto emotivo su cui vorresti lavorare o migliorare?"
                };
            }
            else if (personalsGrowthKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La crescita personale è un viaggio continuo. Quali sono gli obiettivi che ti sei imposto per la tua crescita personale?",
                "Svilupparsi personalmente richiede impegno e dedizione. Ci sono nuove abilità o competenze che stai cercando di sviluppare?",
                "La realizzazione personale può trasformare la percezione della propria vita. Quali passi hai compiuto recentemente verso la tua autorealizzazione?",
                "Ogni passo verso il miglioramento personale è un progresso. Vuoi condividere una tua recente vittoria personale?",
                "Esplorare nuove aree di crescita può essere entusiasmante. C'è un'area della tua vita in cui desideri evolvere ulteriormente?",
                "La crescita personale può venire da molte fonti. Hai scoperto recentemente qualcosa che ti ha ispirato a migliorare te stesso?",
                "Raggiungere l'evoluzione personale è un obiettivo che richiede riflessione e azione. Quali riflessioni ti hanno guidato di recente?"
                };
            }
            else if (relationshipInsightsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Comprendere le dinamiche relazionali può migliorare notevolmente le tue interazioni. Vuoi discutere di una dinamica specifica che hai osservato?",
                "Le intuizioni relazionali possono offrire nuove prospettive su come interagiamo con gli altri. C'è una relazione che ti ha dato nuovi spunti ultimamente?",
                "Migliorare le relazioni interpersonali è un processo continuo. Quali strategie stai utilizzando per migliorare le tue relazioni?",
                "Le relazioni sono complesse e ricche di insegnamenti. Hai imparato qualcosa di importante sulle relazioni recentemente?",
                "Discutere delle relazioni può aiutare a comprendere meglio se stessi e gli altri. C'è un aspetto delle tue relazioni interpersonali che vorresti esplorare?",
                "Le intuizioni sulle relazioni possono essere illuminanti. C'è stato un evento relazionale di recente che ti ha fatto riflettere?",
                "Comprendere meglio le relazioni può portare a una maggiore empatia e connessione. Vuoi condividere un'esperienza recente che ha influenzato la tua visione delle relazioni?"
                };
            }

            else if (selfReflectionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La riflessione personale è un passo importante per comprendere meglio se stessi. Qual è stata una recente scoperta che hai fatto su di te?",
                "L'autoanalisi può aiutarti a capire le tue reazioni e i tuoi sentimenti. Vuoi parlare di un'esperienza recente che ti ha fatto riflettere?",
                "L'introspezione è una potente strumento di crescita personale. C'è un aspetto del tuo comportamento che vorresti esplorare o migliorare?",
                "Focalizzarsi sulla consapevolezza di sé può portare a miglioramenti significativi nella vita quotidiana. Come stai lavorando per essere più consapevole delle tue emozioni e reazioni?",
                "La riflessione su se stessi può a volte essere sfidante. Ti piacerebbe discutere di un conflitto interno che stai provando a risolvere?",
                "Esplorare i propri pensieri e sentimenti può rivelare nuove intuizioni. C'è un particolare pensiero o sentimento che vorresti discutere?",
                "Autovalutarti ti aiuta a comprendere meglio i tuoi punti di forza e di debolezza. Quali sono le qualità che ti piacerebbe potenziare?"
                };
            }
            else if (moodManagementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Gestire efficacemente il proprio umore è fondamentale per il benessere. Hai tecniche che ti aiutano a regolare le tue emozioni?",
                "La regolazione emotiva può migliorare la tua qualità di vita. Vuoi parlare delle emozioni che trovi difficile gestire?",
                "Mantenere un equilibrio emotivo può essere sfidante. Quali attività ti aiutano a mantenere uno stato d'animo stabile?",
                "Controllare le proprie emozioni non è sempre facile. Ci sono situazioni particolari che scatenano cambiamenti nel tuo umore?",
                "Come fai a stabilizzare il tuo umore durante i giorni difficili? Discuterne può a volte offrire nuove strategie.",
                "Quali metodi usi per mantenere un equilibrio emotivo nella tua vita quotidiana? Condividerli può essere utile per altri.",
                "Le emozioni influenzano la nostra percezione del mondo. Quali tecniche di controllo delle emozioni ti piacerebbe esplorare?"
                };
            }
            else if (copingStrategiesKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Ognuno di noi ha delle tecniche personali per affrontare lo stress. Quali sono le tue strategie di coping preferite?",
                "Le strategie di adattamento sono cruciali nei momenti di stress. Puoi condividere una tecnica che hai trovato particolarmente efficace?",
                "Discutere le proprie tecniche di gestione dello stress può essere molto utile. Quali metodi ti aiutano a gestire le pressioni quotidiane?",
                "Costruire resilienza è fondamentale per superare le sfide della vita. Ci sono pratiche specifiche che ti aiutano a rimanere resiliente?",
                "I meccanismi di adattamento possono variare da persona a persona. Vuoi esplorare nuovi modi per gestire lo stress?",
                "Affrontare situazioni difficili richiede solide strategie di coping. Ti piacerebbe discutere come potenziare le tue tecniche attuali?",
                "Conoscere nuovi metodi di adattamento può offrire ulteriore supporto. Esiste un'area della tua vita dove senti di aver bisogno di migliori strategie di coping?"
                };
            }

            else if (exploreKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Esplorare nuovi argomenti può essere molto stimolante. C'è un campo di studio o un interesse che ti piacerebbe esplorare?",
                "Scoprire nuove conoscenze è una delle gioie dell'apprendimento. Qual è l'ultimo argomento che ti ha affascinato?",
                "Approfondire una materia può portare a grandi scoperte. Hai una particolare area di interesse su cui desideri saperne di più?",
                "Investigare i dettagli di un argomento può rivelare insights nascosti. Vuoi discutere di una recente scoperta che hai fatto?",
                "Esaminare vari aspetti di un tema può offrire una comprensione più completa. Quali sono gli aspetti che trovi più intriganti?",
                "Analizzare le informazioni può aiutarti a formare opinioni ben informate. C'è un'analisi specifica che ti interessa fare?",
                "Sondare più a fondo in una discussione può portare a nuove intuizioni. Vuoi esplorare un dibattito attuale o una questione complicata?"
                };
            }
            else if (timeManagementsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Gestire efficacemente il proprio tempo è essenziale per il successo. Hai strategie specifiche che usi per organizzare la tua giornata?",
                "Pianificare in anticipo può liberare tempo per il relax e il divertimento. Usi qualche strumento di pianificazione come app o agende?",
                "Organizzare i compiti in base alla priorità può aiutarti a ridurre lo stress. Come decidi quali attività sono più urgenti?",
                "Programmare il tempo può sembrare rigido, ma può darti grande libertà. Hai una routine giornaliera che segui?",
                "Ottimizzare il tempo può significare delegare o eliminare compiti non essenziali. Hai compiti che potresti delegare ad altri?",
                "L'allocazione efficace del tempo è la chiave per una vita equilibrata. Stai bilanciando bene lavoro e tempo libero?",
                "Scheduling preciso ti aiuta a non sentirti sopraffatto. Qual è il tuo metodo preferito per gestire il calendario?"
                };
            }
            else if (stressReliefKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Trovare modi efficaci per alleviare lo stress è vitale per la salute. Quali attività trovi più rilassanti?",
                "La riduzione dello stress è essenziale per mantenere una buona salute. Pratichi regolarmente esercizi di rilassamento?",
                "Il rilassamento non è solo piacevole, è anche necessario. Che tipo di tecniche di distensione preferisci?",
                "Calmare la mente può essere difficile, ma ci sono molti modi per farlo. Hai provato la meditazione o lo yoga?",
                "Rasserenare lo spirito è tanto importante quanto curare il corpo. Quali sono le tue tecniche preferite per trovare la serenità?",
                "La decompressione dopo una lunga giornata è essenziale. Cosa fai per rilassarti dopo il lavoro o lo studio?",
                "Utilizzare metodi per ridurre lo stress può migliorare notevolmente la qualità della vita. Hai esplorato nuovi metodi recentemente?"
                };
            }

            else if (talkKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Parlare è un ottimo modo per condividere idee e sentimenti. C'è qualcosa di specifico di cui vuoi parlare?",
                "Una buona conversazione può illuminare la giornata. Qual è l'argomento che ti interessa discutere oggi?",
                "Comunicare apertamente è fondamentale in tutte le relazioni. Ti senti a tuo agio a esprimere i tuoi pensieri?",
                "Discutere di vari argomenti può arricchire la nostra conoscenza. Vuoi iniziare una discussione su un tema specifico?",
                "Esprimersi chiaramente è importante. Hai bisogno di consigli su come migliorare le tue abilità comunicative?"
                };
            }
            else if (chatKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Chiacchierare può essere rilassante e piacevole. Di cosa ti piacerebbe parlare per svagarti?",
                "Una conversazione leggera può migliorare l'umore. Hai qualche novità divertente o un aneddoto da condividere?",
                "Battere il chiodo su argomenti casuali può essere un ottimo modo per rilassarsi. Qual è l'ultima cosa interessante che hai sentito?",
                "Gossip o non, a volte è solo bello parlare di cose non troppo serie. C'è qualcosa che ti ha colpito ultimamente?",
                "Dialogare su argomenti leggeri può essere molto piacevole. Vuoi parlare di qualche hobby o interesse recente?"
                };
            }
            else if (conversationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Avere una conversazione significativa può essere molto arricchente. Vuoi approfondire un argomento particolare?",
                "Dialogare con qualcuno può aprire nuove prospettive. C'è un tema su cui ti piacerebbe sentire opinioni diverse?",
                "Interloquire su argomenti di interesse comune può rafforzare i legami. Quali sono gli argomenti che trovi più stimolanti?",
                "Scambiare idee è un ottimo modo per apprendere. Quali sono le ultime novità che hai scoperto e che vorresti discutere?",
                "Discorrere apertamente può migliorare la comprensione reciproca. Esiste un problema o una sfida su cui desideri confrontarti?"
                };
            }

            else if (selfCriticismManagementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Essere troppo critici con se stessi può essere scoraggiante. Quali sono le cose che di solito ti critichi più duramente?",
                "È importante praticare l'auto-compassione. Cosa potresti fare per essere più gentile con te stesso quando commetti errori?",
                "Riconoscere i propri successi può aiutare a bilanciare l'autocritica. Quali sono alcuni successi recenti che non hai ancora celebrato?",
                "L'autocritica può essere trasformata in autovalutazione costruttiva. Vuoi discutere di come puoi trasformare il giudizio interno in feedback utile?",
                "Superare l'autocritica è un passo verso l'autostima. Hai considerato parlare con un coach o un terapeuta per esplorare ulteriormente questo?"
                };
            }
            else if (emotionalResilienceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La resilienza emotiva ci aiuta a navigare attraverso le sfide. Puoi condividere un'esperienza in cui la tua resilienza emotiva è stata messa alla prova?",
                "Aumentare la forza interiore richiede pratica e pazienza. Quali attività ti aiutano a rafforzare la tua stabilità emotiva?",
                "Gestire le emozioni in modo efficace è fondamentale. Quali strategie usi per mantenere la calma in situazioni stressanti?",
                "La resilienza emotiva può essere migliorata attraverso la mindfulness e la meditazione. Hai esperienze con queste pratiche?",
                "Avere una rete di supporto forte può aumentare la resilienza emotiva. Chi sono le persone su cui puoi sempre contare per supporto?"
                };
            }
            else if (dailyWellbeingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il benessere quotidiano è fondamentale per la felicità a lungo termine. Quali abitudini quotidiane ti fanno sentire al meglio?",
                "Mantenere una routine salutare può aumentare significativamente la qualità della vita. Quali sono i pilastri della tua routine quotidiana?",
                "Vivere bene ogni giorno richiede intenzionalità. Quali piccoli cambiamenti potresti fare per migliorare ulteriormente il tuo benessere quotidiano?",
                "Riflettere su cosa ci fa stare bene può essere molto utile. Quali sono le attività o i momenti della giornata che ti rendono più felice?",
                "È importante prendersi cura di sé. Quali pratiche di autocura hai integrato nella tua routine quotidiana per promuovere il benessere?"
                };
            }

            else if (angerManagementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La gestione della rabbia è cruciale per mantenere relazioni sane. Hai tecniche specifiche che usi quando ti senti arrabbiato?",
                "Controllare la rabbia può migliorare significativamente la tua qualità di vita. Vuoi discutere di strategie per calmarti in momenti di forte emotività?",
                "È importante trovare modi sani per esprimere la rabbia. Quali attività trovi utili per rilasciare la tensione in modo produttivo?",
                "Affrontare e calmare la rabbia è un passo importante per il benessere personale. C'è stato un recente evento che ti ha fatto sentire particolarmente furioso?",
                "A volte, capire le radici della nostra rabbia può aiutarci a gestirla meglio. Vuoi esplorare cosa scatena la tua rabbia e come puoi affrontarla?"
                };
            }
            else if (seekingInspirationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Trovare ispirazione può essere trasformativo. Ci sono persone o attività che generalmente ti ispirano?",
                "Quando la motivazione scarseggia, cercare nuovi stimoli creativi può aiutare. Vuoi suggerimenti su come trovare nuove fonti di ispirazione?",
                "L'ispirazione può venire da molte fonti, sia internamente che dall'esterno. Quali sono le cose che ti motivano di più a migliorare e innovare?",
                "Mantenere un flusso costante di ispirazione può essere una sfida. Ti aiuterebbe parlare di tecniche per mantenere alta la tua motivazione?",
                "A volte, basta un piccolo cambiamento per rinnovare la nostra ispirazione. C'è qualcosa di nuovo che vorresti provare per stimolare la tua creatività?"
                };
            }
            else if (improvingCommunicationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Migliorare le abilità comunicative può rafforzare ogni aspetto della vita. Ci sono aree specifiche della tua comunicazione che vorresti migliorare?",
                "Comunicare in modo efficace è fondamentale. Ti capita di trovarti in difficoltà quando esprimi i tuoi pensieri o sentimenti?",
                "Le buone abilità comunicative aiutano a costruire relazioni più forti. Vuoi esplorare strategie per parlare più chiaramente e ascoltare attivamente?",
                "Migliorare la comunicazione può prevenire molti malintesi. Ci sono situazioni recenti in cui avresti voluto comunicare diversamente?",
                "Una comunicazione efficace implica sia parlare che ascoltare. Quali metodi trovi più utili per assicurarti che entrambe le parti si sentano ascoltate?"
                };
            }

            else if (relationshipManagementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Gestire le relazioni può essere complicato ma è essenziale per una vita felice. Qual è la principale sfida relazionale che stai affrontando ora?",
                "Migliorare le relazioni richiede comprensione e pazienza. Vuoi discutere di strategie specifiche per rafforzare i tuoi legami?",
                "Avere relazioni sane è fondamentale per il benessere. Ci sono abitudini o comportamenti che stai cercando di cambiare per migliorare le tue relazioni?",
                "I consigli relazionali possono essere utili. Hai bisogno di suggerimenti su come affrontare una situazione difficile con qualcuno vicino a te?",
                "Le relazioni sono un lavoro di squadra. Come pensi di poter contribuire a creare un ambiente più sano e supportivo nelle tue relazioni?"
                };
            }
            else if (selfEsteemKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'autostima è la chiave per una vita soddisfacente. Quali sono le qualità che apprezzi di più in te stesso?",
                "Costruire fiducia in sé è un processo continuo. Ci sono attività o pratiche che ti aiutano a sentirti più sicuro di te?",
                "Riconoscere il proprio valore personale può cambiare la prospettiva di vita. Che passi stai prendendo per valorizzarti ogni giorno?",
                "L'amor proprio è essenziale. Hai strategie che usi per migliorare la tua autostima quando ti senti giù?",
                "Sentirsi valorizzati è importante. Come cerchi di mantenere un senso di valorizzazione personale nelle tue interazioni quotidiane?"
                };
            }
            else if (overcomingLonelinessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Superare la solitudine è importante per la salute mentale. Quali attività ti aiutano a sentirti più connesso con gli altri?",
                "Sentirsi soli può essere difficile. Hai considerato unirti a gruppi o club dove potresti incontrare persone con interessi simili?",
                "Creare connessioni sociali significative può essere un ottimo antidoto alla solitudine. C'è qualcuno con cui vorresti riallacciare i rapporti?",
                "Fare nuove amicizie può essere una sfida. Vuoi qualche consiglio su come approcciarti e iniziare conversazioni?",
                "Combattere la solitudine richiede iniziativa e apertura. Quali passi potresti prendere per essere più attivo nella tua comunità o online?"
                };
            }

            else if (optimismoKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'ottimismo può illuminare anche i giorni più bui. Qual è qualcosa di positivo che è successo recentemente nella tua vita?",
                "Mantenere un atteggiamento positivo può cambiare la nostra prospettiva su molte cose. Come riesci a mantenere la speranza anche nei momenti difficili?",
                "Vedere il lato positivo delle situazioni può essere una potente abitudine. Ci sono tecniche che usi per rafforzare il tuo pensiero positivo?",
                "La positività è contagiosa. Come condividi il tuo ottimismo con gli altri intorno a te?",
                "La speranza è essenziale per superare le sfide. Cosa ti dà speranza e ti motiva a guardare avanti con ottimismo?"
                };
            }
            else if (forgivenessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il perdono è un atto di forza, non di debolezza. C'è qualcuno che stai cercando di perdonare in questo momento?",
                "Lasciar andare il rancore può portare una grande pace interiore. Hai trovato difficile percorrere il cammino verso il perdono?",
                "Perdonare può liberare molto spazio emotivo per crescere. Vuoi condividere come il perdono ha influenzato la tua vita?",
                "La reconciliazione può essere un processo potente. C'è una situazione che hai recentemente risolto attraverso il perdono?",
                "Superare il rancore può migliorare la nostra salute emotiva e fisica. Quali passi stai prendendo per lavorare sul perdono?"
                };
            }
            else if (adaptingChangeKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Adattarsi al cambiamento è una delle abilità più importanti nella vita. Come stai gestendo le recenti modifiche nella tua vita?",
                "Il cambiamento può essere intimidatorio ma anche eccitante. Ci sono cambiamenti che stai affrontando che ti offrono nuove opportunità?",
                "Essere flessibile e aperto al cambiamento è cruciale. Hai strategie specifiche che ti aiutano ad adattarti ai nuovi scenari?",
                "Accettare il cambiamento può spesso portare a crescita personale. Qual è stato il cambiamento più significativo che hai abbracciato recentemente?",
                "La resilienza al cambiamento richiede coraggio e apertura. C'è un cambiamento che hai trovato particolarmente difficile da accettare?"
                };
            }

            else if (selfCompassionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Praticare l'auto-compassione è fondamentale per il benessere emotivo. Come stai imparando ad essere più gentile con te stesso nei momenti difficili?",
                "Essere gentili con se stessi può essere sfidante ma molto gratificante. Quali passi stai prendendo per trattarti con più amore e comprensione?",
                "L'amore per sé è la base da cui partire per una vita soddisfacente. C'è stata un'occasione recente in cui ti sei concesso perdono e comprensione?",
                "L'autocura è un atto di amore proprio. Quali sono le tue pratiche preferite di autocura che ti aiutano a mantenere un buon equilibrio emotivo?",
                "Perdonarsi è spesso più difficile che perdonare gli altri. Quali tecniche trovi utili per accettare e superare gli errori passati?"
                };
            }
            else if (stressManagementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Gestire efficacemente lo stress è cruciale per la salute fisica e mentale. Quali metodi di rilassamento hai trovato più efficaci?",
                "Quando ci si sente sopraffatti, tecniche di riduzione dello stress possono fare la differenza. Pratichi regolarmente attività come yoga o meditazione?",
                "Mantenere la calma nei momenti di ansia può essere impegnativo. Hai strategie specifiche che usi per calmarti quando ti senti ansioso?",
                "Il rilassamento non è solo un lusso, ma una necessità. Che attività ti aiutano a staccare e rilassarti dopo una giornata stressante?",
                "Affrontare e gestire lo stress a volte richiede un cambiamento di prospettiva. Come ti approcci ai problemi per ridurre l'ansia?"
                };
            }
            else if (spiritualGrowthKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La crescita spirituale può offrire profondi benefici personali. C'è una pratica spirituale che trovi particolarmente arricchente?",
                "Lo sviluppo spirituale spesso accompagna e sostiene la crescita personale. Come integri la spiritualità nel tuo percorso di vita?",
                "La meditazione può essere un potente strumento per la consapevolezza spirituale. Pratichi meditazione regolarmente?",
                "Esplorare il proprio percorso spirituale può essere un viaggio trasformativo. Quali sono state le tue scoperte più significative in questo viaggio?",
                "La spiritualità può connetterci in modi inaspettati con noi stessi e con gli altri. Come ha influenzato la tua spiritualità le tue relazioni personali?"
                };
            }

            else if (resilienceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La resilienza è una delle qualità più forti che possiamo sviluppare. Qual è stata una situazione recente in cui ti sei sentito particolarmente resiliente?",
                "Ricordati che la tua capacità di recuperare dalle difficoltà è una testimonianza della tua forza. Puoi condividere un momento in cui hai superato un ostacolo significativo?",
                "Ogni sfida affrontata è un'altra prova della tua resilienza. Quali strategie ti hanno aiutato a rimanere forte durante i momenti difficili?",
                "La forza interiore si costruisce un'esperienza alla volta. C'è una lezione particolare che hai appreso nel corso di una recente sfida?",
                "La resilienza non è solo superare le difficoltà, ma anche crescere attraverso di esse. Come pensi di essere cambiato dopo l'ultima sfida che hai affrontato?"
                };
            }
            else if (selfRealizationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La realizzazione personale è un viaggio unico per ciascuno di noi. Quali sono gli obiettivi che ti stai ponendo per il tuo sviluppo personale?",
                "Scoprire più su noi stessi è un percorso emozionante. Ci sono aspetti della tua vita che stai esplorando più profondamente in questo periodo?",
                "Raggiungere i propri obiettivi porta grande soddisfazione. Qual è un traguardo che hai recentemente raggiunto e di cui sei particolarmente orgoglioso?",
                "Capire i propri desideri può illuminare il cammino verso la felicità. Hai identificato qualche desiderio o passione che vuoi perseguire con maggiore intensità?",
                "La realizzazione personale spesso deriva dal bilanciare i propri sogni con la realtà quotidiana. Come stai lavorando per realizzare i tuoi sogni?"
                };
            }
            else if (overcomingChallengesKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Affrontare e superare le sfide può essere incredibilmente gratificante. Qual è una sfida che stai affrontando attualmente e come stai cercando di superarla?",
                "Ogni problema presenta un'opportunità di crescita. Hai degli esempi di come sei riuscito a trasformare una difficoltà in un successo?",
                "Gestire gli ostacoli è una competenza preziosa. Quali metodi o risorse trovi più efficaci nel superare gli ostacoli?",
                "Vincere le difficoltà richiede spesso creatività e determinazione. Puoi condividere una strategia che ti ha aiutato a superare un ostacolo particolarmente complesso?",
                "Riflettere su come abbiamo gestito problemi passati può darci fiducia per il futuro. Qual è un ricordo di una sfida che hai superato che ti dà forza ora?"
                };
            }
            else if (expressionsOfEnthusiasmKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Fantastico! È bello vedere tanto entusiasmo.",
                "Sono felice che tu sia così carico! Come posso contribuire alla tua emozione?",
                "Che bello sentire che non vedi l'ora! C'è qualcosa di specifico che aspetti con impazienza?"
                };
            }
            else if (expressionsOfHesitationKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "È normale avere dei dubbi. Vuoi discuterne per trovare più chiarezza?",
                "Prenditi il tempo che ti serve per decidere. Posso aiutarti in qualche modo?",
                "Capisco l'esitazione. A volte una piccola pausa può aiutare a vedere le cose più chiaramente."
                };
            }
            else if (expressionsOfComfortKeywords.Any(k => message.ToLower().Contains(k)))
            {
                responses = new string[]
                {
                "Capisco come ti senti e sono qui per te.",
                "Non preoccuparti, le cose si sistemeranno. Come posso supportarti al meglio?",
                "Tranquillo, comprendo la situazione. Se c'è qualcosa che posso fare, fammelo sapere."
                };
            }

            else if (emotionalSupportKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Capisco che possa essere un periodo difficile. Sono qui per te. Vuoi parlare di cosa ti sta facendo sentire così?",
                "Mi dispiace che tu ti senta giù. A volte, condividere ciò che provi può alleggerire il peso. Cosa ti preoccupa di più in questo momento?",
                "Non sei solo, sono qui per ascoltarti. Qual è la cosa che ti pesa di più ora? Forse posso aiutarti a trovare una nuova prospettiva.",
                "È completamente normale sentirsi sopraffatti a volte. Vuoi provare a raccontarmi di più su ciò che ti fa sentire così?",
                "Tutti abbiamo momenti difficili. Se vuoi, possiamo esplorare insieme i tuoi sentimenti e vedere se troviamo un modo per farti sentire meglio."
                };
            }
            else if (encouragementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Ricorda che ogni grande viaggio inizia con un piccolo passo. Sei più forte di quanto pensi e hai il potere di superare ogni sfida.",
                "Sai, la resilienza si costruisce affrontando le sfide, non evitandole. Sei capace di molto, e credo in te. Qual è il tuo prossimo passo?",
                "Non lasciare che la paura di fallire ti fermi. Ogni errore è un passo verso il successo. Come posso aiutarti a prendere il prossimo passo?",
                "Ti incoraggio a tenere duro. Anche le giornate più buie portano a tramonti luminosi. C'è qualcosa di specifico che ti aiuterebbe a sentirte meglio oggi?",
                "Credi in te stesso come io credo in te. Hai tutte le qualità per riuscire. Quali sono i tuoi sogni e come posso aiutarti a realizzarli?"
                };
            }
            else if (personalReflectionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'auto-riflessione è un potente strumento di crescita. Che scoperte hai fatto su te stesso recentemente?",
                "Comprendere noi stessi è il primo passo verso il miglioramento. Ci sono aree della tua vita che senti di voler esplorare o cambiare?",
                "L'autoconsapevolezza può illuminare percorsi che non sapevamo di poter percorrere. Vuoi discutere di qualcosa che hai scoperto su te stesso?",
                "L'introspezione può a volte essere difficile, ma è così gratificante. Ci sono domande su te stesso a cui stai cercando risposte?",
                "Riflettere su noi stessi ci permette di capire meglio i nostri bisogni e desideri. Qual è la cosa più importante che hai appreso su di te ultimamente?"
                };
            }

            else if (sustainabilityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La sostenibilità è fondamentale per il nostro futuro. Quali pratiche ecologiche hai integrato nella tua vita quotidiana?",
                "Ci sono molti modi per contribuire alla salute del nostro pianeta. Ti interessano particolarmente il riciclo o il risparmio energetico?",
                "Adottare uno stile di vita più verde può essere un viaggio interessante. Hai esplorato opzioni come l'energia rinnovabile o i prodotti zero waste?",
                "L'ecologia è una passione condivisa da molti. Partecipi a qualche iniziativa comunitaria legata alla sostenibilità?",
                "Rispettare l'ambiente è un impegno importante. Qual è la tua motivazione principale per vivere in modo più sostenibile?"
                };
            }
            else if (technologyAdvancementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il mondo della tecnologia è in costante evoluzione. Quali recenti innovazioni trovi più eccitanti?",
                "I gadget moderni possono trasformare il modo in cui viviamo e lavoriamo. Hai recentemente scoperto un dispositivo che ha cambiato la tua routine quotidiana?",
                "Il futuro della tecnologia promette grandi cambiamenti. Ci sono particolari sviluppi tecnologici che ti interessano o ti preoccupano?",
                "La tecnologia può migliorare la vita in molti modi. Stai sfruttando la tecnologia per migliorare la tua efficienza personale o professionale?",
                "Con l'avanzare della tecnologia, emergono nuove sfide. Come pensi che dovremmo affrontare questioni come la privacy o la sicurezza in questo nuovo contesto?"
                };
            }
            else if (culinaryTravelKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "I viaggi culinari offrono un modo unico di esplorare il mondo. Qual è stata la tua esperienza gastronomica più memorabile durante un viaggio?",
                "Assaporare cucine internazionali può essere un'avventura in sé. C'è una particolare cucina del mondo che ti piacerebbe esplorare di più?",
                "Ogni cultura ha i suoi piatti distintivi. Hai mai provato a cucinare ricette internazionali a casa? Quali?",
                "Il cibo è spesso al centro delle esperienze di viaggio. Hai qualche destinazione da sogno dove vorresti andare principalmente per la cucina?",
                "Condividere pasti da diverse culture può aprire la mente. Ti piace sperimentare con nuovi sapori e ingredienti?"
                };
            }

            else if (currentEventsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Rimanere informati sugli eventi recenti è importante. Quali tipi di notizie ti interessano di più?",
                "Le notizie possono influenzare significativamente la nostra visione del mondo. Come ti assicuri di ricevere informazioni equilibrate e accurate?",
                "Gli aggiornamenti mondiali possono essere travolgenti. Preferisci discutere di argomenti specifici o avere una panoramica generale?",
                "Seguire le ultime notizie può aiutare a capire meglio il contesto globale. C'è un evento recente che ti ha particolarmente colpito o interessato?",
                "Discutere di attualità può essere molto stimolante. C'è una particolare area geografica o un tema che segue più da vicino?"
                };
            }
            else if (emotionalWellbeingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Il benessere emotivo è fondamentale per una vita soddisfacente. Quali pratiche quotidiane adotti per mantenere la tua salute mentale?",
                "Gestire le proprie emozioni in modo efficace è una competenza cruciale. Hai strategie specifiche che ti aiutano a gestire lo stress o l'ansia?",
                "Trovare un equilibrio emotivo può essere sfidante. Ci sono attività o hobby che trovi particolarmente utili per la tua stabilità emotiva?",
                "La salute mentale è tanto importante quanto quella fisica. C'è qualcosa che ti preoccupa ultimamente e di cui vorresti parlare?",
                "Ognuno ha il proprio modo di affrontare i momenti difficili. Vuoi condividere come fai fronte nei momenti di turbamento emotivo?"
                };
            }
            else if (financialManagementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La gestione delle finanze personali è essenziale per garantire un futuro sicuro. Hai già un piano di budgeting che segui?",
                "I consigli finanziari possono variare molto a seconda delle situazioni personali. C'è un aspetto delle tue finanze che ti preoccupa particolarmente?",
                "Fare investimenti informati è un ottimo modo per far crescere i propri risparmi. Stai considerando qualche investimento specifico al momento?",
                "Avere un buon controllo sulle proprie finanze può alleviare molti stress. Utilizzi strumenti o app per aiutarti nella gestione del denaro?",
                "Il budgeting è una parte fondamentale della gestione finanziaria. Quali strategie hai trovato più efficaci per gestire le tue spese?"
                };
            }

            else if (travelPlanningKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Pianificare un viaggio è sempre eccitante! Hai già in mente una destinazione o cerchi suggerimenti?",
                "Viaggiare apre la mente a nuove culture e esperienze. Qual è lo scopo del tuo viaggio? Avventura, relax, cultura, o altro?",
                "Assicurati di considerare tutti gli aspetti pratici del viaggio, come il budget, l'alloggio e i trasporti. Hai bisogno di aiuto per organizzare queste componenti?",
                "Esplorare nuovi luoghi è un ottimo modo per arricchire la propria vita. Ci sono attività specifiche che vorresti fare durante il tuo viaggio?",
                "Ricorda di lasciare spazio per l'imprevisto, che spesso si rivela la parte più memorabile di un viaggio. Hai già pensato a un itinerario flessibile?"
                };
            }
            else if (learningOpportunitiesKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'apprendimento continuo è fondamentale per la crescita personale e professionale. C'è un'area specifica in cui vorresti migliorare o imparare qualcosa di nuovo?",
                "Ci sono molti corsi disponibili online che possono offrire nuove opportunità di apprendimento. Sei interessato a corsi online o preferisci lezioni in presenza?",
                "Mantenersi aggiornati con la formazione può aprire nuove porte nel mondo del lavoro. Hai considerato di aggiornare le tue competenze con qualche nuovo corso?",
                "L'educazione non si ferma mai. C'è un campo di studi che hai sempre desiderato esplorare?",
                "Studiare può anche essere un'attività di gruppo molto stimolante. Hai amici o colleghi interessati a imparare insieme a te?"
                };
            }
            else if (mindfulnessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La mindfulness è un potente strumento per migliorare il benessere mentale e ridurre lo stress. Pratichi già qualche forma di meditazione?",
                "Incorporare momenti di consapevolezza nella giornata può aiutare a mantenere la calma e la concentrazione. Vuoi qualche consiglio su come iniziare?",
                "La meditazione può sembrare difficile all'inizio, ma anche pochi minuti al giorno possono fare una grande differenza. Ti interessa un'introduzione a tecniche di base?",
                "Cultivare la calma interiore è essenziale in un mondo frenetico. Hai delle tecniche particolari che utilizzi per rilassarti e ritrovare il centro?",
                "La mindfulness può essere praticata in molti modi, incluso attraverso camminate consapevoli, yoga o semplicemente respirando profondamente. Qual è il tuo metodo preferito?"
                };
            }

            else if (hobbiesInterestsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Gli hobby e gli interessi possono arricchire la nostra vita. Quali sono le attività che ami fare nel tuo tempo libero?",
                "Avere un passatempo che ti appassiona può essere molto gratificante. C'è un hobby che hai scoperto di recente?",
                "Il tempo libero è prezioso. Come preferisci trascorrerlo per rilassarti o per divertirti?",
                "Condividere attività preferite può creare legami forti. Hai amici o familiari con cui condividi i tuoi interessi?",
                "A volte esplorare nuovi hobby può aprire nuovi orizzonti. C'è qualcosa che hai sempre voluto provare ma non hai ancora avuto l'opportunità?"
                };
            }
            else if (personalRelationshipsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Le relazioni personali sono la base della nostra vita sociale. Come gestisci le tue amicizie e i rapporti familiari?",
                "La qualità dei nostri rapporti interpersonali può influenzare molto la nostra felicità. C'è qualcuno con cui desideri migliorare la comunicazione?",
                "Mantenere relazioni sane richiede impegno e comprensione. Quali strategie trovi utili per rafforzare i legami con gli altri?",
                "È importante avere un supporto emotivo. Ti senti supportato dalle tue relazioni personali?",
                "Le amicizie possono arricchire la nostra esperienza di vita. C'è un'amicizia recente che trovi particolarmente significativa?"
                };
            }
            else if (timeManagementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Gestire efficacemente il proprio tempo è una competenza chiave per il successo. Quali metodi utilizzi per organizzare la tua giornata?",
                "La pianificazione può aiutare a massimizzare l'efficienza. Utilizzi strumenti o app per aiutarti con la gestione del tempo?",
                "Avere una buona gestione del tempo permette di liberare spazio per ciò che conta davvero. Quali attività hai ottimizzato di recente?",
                "Ottimizzare il tempo a disposizione può portare a una vita meno stressante. Hai trovato un equilibrio tra lavoro e tempo libero?",
                "Essere organizzati può semplificare molti aspetti della vita. C'è un consiglio sulla gestione del tempo che hai trovato particolarmente utile?"
                };
            }

            else if (personalGrowthKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La crescita personale è un viaggio emozionante e senza fine. Quali aspetti della tua vita stai cercando di migliorare attualmente?",
                "L'auto-miglioramento è spesso guidato dalla volontà di superare nuove sfide. Quali obiettivi ti sei posto per il tuo sviluppo personale?",
                "Esplorare il proprio potenziale può essere molto gratificante. Hai trovato risorse o strumenti particolarmente utili nel tuo percorso di autorealizzazione?",
                "Migliorarsi richiede impegno e dedizione. Quali sono state le tue strategie più efficaci per mantenere la motivazione?",
                "Il percorso verso l'auto-miglioramento è personale e unico. Vuoi condividere una lezione importante che hai imparato di recente?"
                };
            }
            else if (careerAdviceKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Navigare nel mondo della carriera può essere complesso. C'è un'area specifica in cui stai cercando consigli?",
                "L'orientamento professionale può fare una grande differenza. Hai considerato quali potrebbero essere i prossimi passi nella tua carriera?",
                "Sviluppare la propria carriera è un processo continuo. Quali competenze stai cercando di sviluppare o quali ruoli ti interessano?",
                "Le decisioni professionali sono spesso cruciali. Stai affrontando qualche scelta difficile al momento?",
                "L'avanzamento nella carriera richiede strategia e talvolta anche un po' di audacia. Vuoi discutere di possibili mosse o cambiamenti che stai considerando?"
                };
            }
            else if (healthWellnessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Mantenere la salute e il benessere è essenziale per una vita lunga e felice. Hai una routine di benessere che segui regolarmente?",
                "A volte, piccoli cambiamenti nello stile di vita possono portare grandi benefici. Hai recentemente adottato nuove abitudini salutari?",
                "Prendersi cura della propria salute è un investimento nel proprio futuro. Ci sono particolari obiettivi di salute che stai perseguendo?",
                "Il benessere fisico è strettamente legato a quello mentale. Come equilibri entrambi gli aspetti nella tua vita quotidiana?",
                "Vivere sano non è solo una questione di dieta e esercizio fisico, ma anche di gestione dello stress e riposo. Quali metodi trovi più efficaci per rilassarti e ricaricarti?"
                };
            }

            else if (movieRecommendationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Assolutamente! Se non hai ancora visto 'Dune', te lo consiglio vivamente. È un'epica avventura di fantascienza che ha riscosso grande successo.",
                "Ti suggerisco di guardare 'No Time to Die', l'ultimo film di James Bond. È pieno di azione e ha ricevuto ottime recensioni.",
                "Se ti piacciono i film drammatici, 'Nomadland' potrebbe interessarti. Ha vinto numerosi premi ed è molto toccante.",
                "Per una serata leggera, 'Soul' di Pixar è un'ottima scelta. È una storia meravigliosa che esplora temi profondi in modo accessibile a tutti.",
                "Se sei un appassionato di thriller, 'Tenet' di Christopher Nolan ti terrà incollato allo schermo. È un film che combina azione intensa e una trama ingegnosa."
                };
            }
            else if (musicRecommendationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Prova ad ascoltare l'album 'After Hours' di The Weeknd. È ricco di hit e ha ricevuto elogi dalla critica.",
                "Se ti piace il pop, ti suggerisco 'Future Nostalgia' di Dua Lipa. È un album che ha dominato le classifiche di tutto il mondo.",
                "Per gli amanti del rock, 'Power Up' degli AC/DC è un must. È un ritorno alle radici del rock'n'roll con energia pura.",
                "Billie Eilish con il suo album 'When We All Fall Asleep, Where Do We Go?' offre un mix unico di pop, elettronica e elementi dark.",
                "Se preferisci qualcosa di più tranquillo, 'Folklore' di Taylor Swift è un'ottima scelta. È un album che esplora temi del folklore in chiave moderna."
                };
            }
            else if (bookRecommendationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Ti consiglierei di leggere 'Le 7 regole per avere successo' di Stephen R. Covey. È un classico dell'auto-miglioramento che offre consigli preziosi.",
                "Un altro ottimo libro è 'Niente può fermarti' di David Goggins. È una lettura motivante che ti spingerà a superare i tuoi limiti.",
                "Se ti interessa migliorare le tue relazioni interpersonali, 'Come trattare gli altri e farseli amici' di Dale Carnegie è perfetto.",
                "Per comprendere l'importanza delle abitudini nella nostra vita, 'Il potere delle abitudini' di Charles Duhigg è altamente consigliato.",
                "Se ti piacciono le storie di trasformazione personale, 'Atomic Habits' di James Clear è un libro che offre strategie pratiche per cambiare le tue abitudini passo dopo passo."
                };
            }

            else if (greetingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Ciao! Come posso assisterti oggi?",
                "Buongiorno! È sempre un piacere parlare con te. C'è qualcosa di specifico di cui vuoi parlare?",
                "Ehilà! Spero tu stia avendo una giornata fantastica. Come posso aiutarti?",
                "Salve! Ci sono novità che vorresti condividere con me oggi?",
                "Buonasera! Come posso rendere la tua serata ancora migliore?"
                };
            }
            else if (farewellKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Arrivederci! È stato bello parlare con te. A presto!",
                "Addio, spero di rivederti presto! Ricorda, sono qui se hai bisogno di ulteriore assistenza.",
                "Ciao, ti auguro una giornata meravigliosa! Non esitare a tornare se hai altre domande.",
                "Buonanotte! Riposa bene e non dimenticare che sono qui per aiutarti quando ne hai bisogno.",
                "A presto! Se hai bisogno di altro, sai dove trovarmi."
                };
            }
            else if (thanksKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Di nulla, sono qui per aiutarti!",
                "È sempre un piacere assisterti, non c'è bisogno di ringraziare!",
                "Sono contento di essere stato d'aiuto. Non esitare a chiedere se hai bisogno di altro!",
                "Grazie a te per avermi dato l'opportunità di assisterti!",
                "L'apprezzamento è reciproco! Fai pure altre domande se ne hai."
                };
            }

            else if (calmnessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Mantenere la calma è fondamentale in molti aspetti della vita. Quali tecniche usi per restare tranquillo?",
                "La quiete interiore è un vero tesoro. Quali attività ti aiutano a riconnetterti con la tua serenità?",
                "Raggiungere uno stato di calma può migliorare il benessere. Hai rituali giornalieri che ti aiutano a mantenere la placidità?",
                "La tranquillità può essere influente. Come influisce il tuo stato di calma sulle persone intorno a te?",
                "Essere sereni può aprire la mente a nuove idee. Cosa ti aiuta a mantenere la calma nei momenti di stress?"
                };
            }
            else if (enthusiasmKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'entusiasmo è contagioso! Cosa ti ha recentemente appassionato così tanto?",
                "La passione è il carburante per il successo. Quali sono i tuoi interessi o progetti appassionati al momento?",
                "Niente batte il sentimento di vivacità quando si fa ciò che si ama. Puoi condividere un'esperienza recente in cui hai sentito un grande ardore?",
                "Con cosa ti senti particolarmente zelante ultimamente? Sarebbe fantastico saperne di più sulle tue passioni!",
                "L'ardore nel perseguire i propri obiettivi può fare una grande differenza. Quali sono le tue aspirazioni che ti ispirano entusiasmo?"
                };
            }
            else if (confusionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi confusi è parte del processo di apprendimento e crescita. C'è qualcosa di specifico che ti sta confondendo ora?",
                "La disorientazione può essere un segno che stiamo affrontando nuove sfide. Vuoi discutere di ciò che ti rende incerto?",
                "Affrontare la confusione può essere difficile. Ci sono decisioni o situazioni recenti che ti hanno lasciato perplesso?",
                "A volte, prendersi un momento per riflettere può aiutare a chiarire le cose. Hai strategie che ti aiutano a gestire quando ti senti confuso?",
                "La confusione non è sempre negativa; può indicare che stai cercando di capire qualcosa di profondo. Hai bisogno di supporto per navigare in questo?"
                };
            }

            else if (noveltyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La novità può essere stimolante e rinvigorente. Qual è la novità più eccitante nella tua vita ora?",
                "Incorporare elementi nuovi e originali può rinfrescare la nostra routine quotidiana. Hai aggiunto qualcosa di nuovo di recente alla tua vita?",
                "L'innovazione è alla base del progresso. C'è qualche idea originale che vorresti esplorare?",
                "Esplorare concetti inediti può essere una fonte di ispirazione. C'è un'area in cui stai cercando di innovare?",
                "L'entusiasmo per il nuovo è contagioso. Con chi condividi le tue scoperte più recenti?"
                };
            }
            else if (serendipityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "A volte, un colpo di fortuna può cambiare completamente la nostra prospettiva. Hai avuto recentemente una piacevole sorpresa?",
                "La serendipità può portare gioia inaspettata. C'è stato un evento fortunato ultimamente che ti ha fatto sorridere?",
                "Riconoscere i momenti di fortuna nella nostra vita può aumentare la gratitudine. Cosa ti è capitato di recente che consideri un vero colpo di fortuna?",
                "I momenti fortunati spesso diventano memorie preziose. Vuoi condividere un'esperienza recente dove la casualità ha giocato a tuo favore?",
                "Credere nel destino può essere confortante. Pensi che la serendipità abbia un ruolo nel tuo percorso di vita?"
                };
            }
            else if (doubtKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Affrontare i dubbi può essere un segnale di un profondo processo di riflessione. C'è qualcosa in particolare che ti sta facendo esitare?",
                "La perplessità è naturale quando ci troviamo di fronte a decisioni importanti. Vuoi discutere delle tue incertezze?",
                "Esitare prima di fare una scelta può essere prudente. Ci sono decisioni imminenti che ti trovano indeciso?",
                "Gestire l'incertezza può essere stressante. Hai strategie che ti aiutano a navigare attraverso i momenti di dubbio?",
                "I dubbi possono anche essere un'opportunità per un approfondimento. Qual è l'ultimo grande interrogativo che hai affrontato?"
                };
            }

            else if (comfortKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Trovare comfort nelle piccole cose può fare una grande differenza. Qual è la tua consolazione nei momenti difficili?",
                "A volte, un po' di conforto è tutto ciò di cui abbiamo bisogno per sentirsi meglio. Ti piacerebbe parlare di cosa ti fa sentire sollevato?",
                "Il sollievo dai momenti difficili può venire dalle fonti più inaspettate. Hai trovato conforto in qualcosa di recente?",
                "La consolazione nei momenti di bisogno è importante. Come trovi solace nelle situazioni stressanti?",
                "Ricordare che il conforto può arrivare con il tempo. C'è qualcosa che ti aiuterebbe a sentirti più a tuo agio ora?"
                };
            }
            else if (excitementsKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'eccitazione può davvero energizzare la nostra giornata. Cosa ti ha emozionato di recente?",
                "Sentire l'adrenalina può essere elettrizzante. Hai progetti in arrivo che ti stimolano?",
                "Esplorare nuove esperienze può essere molto eccitante. Hai qualcosa di pianificato che ti fa sentire questo brivido?",
                "L'emozione di nuove avventure può mantenere la vita interessante. Qual è l'ultima avventura che hai intrapreso?",
                "A volte, un piccolo brivido è ciò che ci serve per sentirsi vivi. Qual è stata la tua ultima esperienza entusiasmante?"
                };
            }
            else if (fatigueKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La stanchezza può pesare molto su di noi. È importante ascoltare il nostro corpo. Hai considerato di prenderti una pausa?",
                "Affrontare la fatica richiede coraggio e la capacità di riconoscere i propri limiti. Come gestisci la tua energia quotidianamente?",
                "La fatica cronica può essere debilitante. È importante cercare supporto. Hai accesso a risorse che possono aiutarti?",
                "Riposarsi adeguatamente è cruciale. Trovi difficile disconnetterti e rilassarti per ricaricare?",
                "Gestire l'esaurimento può essere una sfida. Ci sono cambiamenti che potresti fare per aiutarti a sentirsi meno affaticato?"
                };
            }

            else if (serenityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La serenità è uno stato d'animo prezioso. Cosa ti aiuta a mantenere la calma nella vita di tutti i giorni?",
                "Sentirsi tranquilli può migliorare notevolmente la nostra qualità della vita. Hai tecniche particolari che usi per raggiungere questa pace?",
                "La calma interiore è una forza. Come riesci a trovare il tuo centro nei momenti turbolenti?",
                "Essere in uno stato di quiete può essere tanto rinfrescante. C'è un luogo speciale dove ti piace andare per ritrovare la tua serenità?",
                "Condividere la propria pace interiore può aiutare anche gli altri a sentirsi più calmi. Pensi di influenzare positivamente l'ambiente intorno a te con la tua tranquillità?"
                };
            }
            else if (discomfortKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Affrontare il disagio può essere una parte importante della crescita personale. Vuoi parlare di ciò che ti fa sentire scomodo?",
                "A volte, riconoscere il nostro malessere può essere il primo passo verso il miglioramento. C'è qualcosa che possiamo fare per aiutarti a sentirti meglio?",
                "Sentirsi a disagio non è piacevole, ma può essere illuminante. Cosa pensi di poter imparare da questa esperienza?",
                "Gestire l'inquietudine può richiedere tempo e pazienza. Hai strategie che ti aiutano a navigare attraverso questi momenti difficili?",
                "Parlare apertamente del proprio malessere può alleviare parte del peso. C'è qualcuno con cui ti senti a tuo agio a discutere di queste sensazioni?"
                };
            }
            else if (vigorKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi pieni di energia è meraviglioso! Cosa ti dà quella spinta di vitalità?",
                "Avere una sensazione di vigore può trasformare completamente la giornata. Quali sono le tue attività preferite per mantenere alto il livello energetico?",
                "È fantastico sentirti così dinamico! Come sfrutti al meglio questa energia nella tua routine quotidiana?",
                "La vitalità è contagiosa. In che modo pensi di poter influenzare positivamente le persone intorno a te con il tuo spirito vivace?",
                "Essere vigorosi può aprire tante opportunità. Hai nuovi progetti o hobby che vuoi esplorare con questa energia rinnovata?"
                };
            }
            else if (melancholyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La malinconia ha una sua dolcezza. Vuoi parlare di ciò che ti rende riflessivo o melanconico?",
                "Anche la tristezza lieve ha il suo posto nella nostra gamma emotiva. Può portare introspezione e crescita personale. Cosa ti ha fatto sentire così ultimamente?",
                "Essere nostalgico può connetterci con ricordi del passato. Ci sono ricordi specifici ai quali stai pensando ora?",
                "Riflettere sui momenti malinconici può a volte portare una nuova comprensione. C'è qualcosa che queste emozioni ti hanno aiutato a realizzare su te stesso o sulla tua vita?",
                "Accettare la malinconia come parte della nostra esperienza umana può aiutare a gestirla meglio. Hai trovato modi per abbracciare questi sentimenti con grazia?"
                };
            }
            else if (optimismKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'ottimismo può illuminare i giorni più bui. Cosa ti rende particolarmente speranzoso ora?",
                "Avere una prospettiva positiva è una forza potente. Come mantieni questo atteggiamento nella vita di tutti i giorni?",
                "È incoraggiante sentire che sei fiducioso riguardo al futuro. Ci sono obiettivi specifici o eventi che attendi con impazienza?",
                "L'ottimismo è contagioso. In che modo pensi di poter diffondere questa positività alle persone intorno a te?",
                "Sentirsi incoraggiati è un ottimo inizio per qualsiasi impresa. C'è un nuovo progetto o hobby che stai considerando di iniziare con questo spirito?"
                };
            }
            else if (reliefKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi sollevati dopo un periodo di tensione è una sensazione incredibile. Vuoi condividere cosa ha portato a questo cambiamento?",
                "Il sollievo può venire in molte forme. Che tipo di attività ti aiuta a sentirti rilassato e liberato dallo stress?",
                "È importante riconoscere e celebrare i momenti di sollievo nella nostra vita. Qual è stata la chiave per superare le tue sfide recenti?",
                "La liberazione dalle preoccupazioni può aprire nuove possibilità. C'è qualcosa di nuovo che prevedi di esplorare ora che ti senti più libero?",
                "Rilassarsi dopo un lungo periodo di stress può essere molto rigenerante. Hai dei piani per approfittare di questa nuova serenità?"
                };
            }
            else if (contentmentKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi contenti è una meravigliosa sensazione di pace e soddisfazione. Cosa contribuisce maggiormente alla tua sensazione di contentezza?",
                "Essere soddisfatti della propria vita è un obiettivo importante. Ci sono aspetti della tua vita che ti fanno sentire particolarmente realizzato?",
                "La felicità e il contentamento spesso vanno di pari passo. Quali piccole cose della vita quotidiana ti rendono felice?",
                "La realizzazione personale è un viaggio unico per ognuno di noi. Quali traguardi hai raggiunto di recente che ti hanno fatto sentire realizzato?",
                "La contentezza può derivare da molte fonti, grandi e piccole. Hai dei rituali o attività che ti aiutano a mantenere questo stato d'animo positivo?"
                };
            }
            else if (anxietyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'ansia può essere difficile da gestire, ma ricordati che non sei solo. Vuoi parlare di ciò che ti preoccupa?",
                "Esistono molte tecniche per gestire l'ansia, come la respirazione profonda o la mindfulness. Hai provato qualche metodo che ti aiuta a rilassarti?",
                "Affrontare l'ansia è un processo, ma è importante ricordare che è possibile sentirsi meglio. Ci sono cose specifiche che scatenano la tua ansia?",
                "Parlare delle tue preoccupazioni può spesso alleviare la tensione. C'è qualcosa in particolare che vorresti discutere?",
                "Ricorda che l'ansia è una reazione normale a stress e incertezze. Tuttavia, prendersi cura di sé può aiutare a gestirla efficacemente. Cosa fai per prenderti cura di te?"
                };
            }
            else if (inspirationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi ispirati può scatenare incredibili onde di creatività. Cosa ti ha ispirato di recente?",
                "L'ispirazione è potente; trasforma idee ordinarie in straordinarie. Vuoi condividere come questa ispirazione ha influenzato i tuoi piani o progetti?",
                "Chi o cosa ti serve spesso da fonte di ispirazione? Sarebbe interessante conoscere le tue muse!",
                "Quando siamo motivati da ispirazione, possiamo raggiungere grandi cose. Qual è il tuo obiettivo attuale che questa ispirazione ti sta aiutando a perseguire?",
                "A volte, anche un piccolo momento o incontro può ispirarci profondamente. Hai avuto uno di questi momenti di recente?"
                };
            }
            else if (nostalgiaKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La nostalgia può essere dolce-amara, ma ci collega anche ai bei ricordi. Qual è un ricordo felice che ti piace rivisitare?",
                "I ricordi del passato possono essere tesori preziosi. Vuoi condividere una storia dal tuo passato che ti fa sorridere?",
                "A volte, riflettere sui momenti passati può offrire conforto. C'è un'epoca o un momento che ti manca particolarmente?",
                "Rimpiangere il passato è naturale, ma è anche importante trovare gioia nel presente. C'è qualcosa di recente che ti ha reso felice?",
                "I ricordi possono essere un rifugio sicuro quando i tempi sono duri. Vuoi parlare di un'esperienza passata che ancora ti influenzia?"
                };
            }
            else if (discontentKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi insoddisfatti può essere il primo passo verso il cambiamento. Cosa ti farebbe sentire più appagato?",
                "Affrontare la delusione è duro, ma può anche essere un'opportunità per riallineare le tue aspettative. Vuoi discutere di come adeguare i tuoi obiettivi?",
                "Esplorare le radici del tuo scontento può fornire intuizioni preziose. C'è un'area specifica della tua vita che vorresti migliorare?",
                "A volte, riconoscere ciò che ci manca può motivarci a fare cambiamenti positivi. Sei pronto a fare il prossimo passo verso quel cambiamento?",
                "La sensazione di insufficienza è spesso un segnale che stiamo ignorando le nostre vere necessità. Vuoi parlare di cosa potrebbe realmente soddisfarti?"
                };
            }
            else if (gratitudeKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Esprimere gratitudine può migliorare il tuo umore e quello delle persone intorno a te. Per cosa sei più grato oggi?",
                "La riconoscenza non solo rende gli altri felici, ma ci aiuta a riconoscere il valore delle cose belle nella nostra vita. Vuoi condividere qualcosa per cui sei grato recentemente?",
                "Apprezzare le piccole cose può fare una grande differenza nel nostro benessere generale. Hai notato dei piccoli piaceri oggi?",
                "La gratitudine può trasformare il comune in straordinario. Qual è un momento ordinario che hai trovato straordinario?",
                "Ricordare di essere grati può aiutare a mantenere un atteggiamento positivo. Hai una routine per praticare la gratitudine?"
                };
            }
            else if (excitementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'entusiasmo è un motore potente che ci spinge verso nuove avventure. C'è qualcosa in particolare che ti sta eccitando ora?",
                "Essere eccitato per un evento futuro può rendere tutto più luminoso. Vuoi condividere cosa aspetti con impazienza?",
                "L'euforia è un'emozione intensa che può riempire di gioia. Qual è stata l'ultima volta che ti sei sentito veramente euforico?",
                "L'emozione per le nuove esperienze è ciò che spesso rende la vita interessante. Hai dei piani emozionanti per il prossimo futuro?",
                "Il fervore per i propri interessi può essere molto gratificante. Quali passioni stai attualmente inseguendo?"
                };
            }
            else if (serenityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La serenità è un tesoro prezioso nella nostra vita frenetica. Quali attività ti aiutano a mantenere la calma?",
                "Trovare la pace interiore è essenziale per una vita equilibrata. Pratichi qualche tecnica di meditazione o mindfulness?",
                "La tranquillità può migliorare significativamente la qualità della vita. Hai dei luoghi speciali dove ti senti particolarmente in pace?",
                "Essere rilassato non è solo piacevole, ma anche salutare per mente e corpo. Come ti assicuri di trovare tempo per rilassarti?",
                "La calma è spesso contagiosa. Quando sei tranquillo, anche le persone intorno a te tendono a sentirsi più serene. Hai notato questo effetto?"
                };
            }
            else if (empathyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'empatia è la capacità di sentire con gli altri, di mettersi nei loro panni. È un bel gesto che vuoi svilupparla ulteriormente.",
                "Comprendere gli altri può arricchire le tue relazioni e aiutarti a connetterti su un livello più profondo. Vuoi parlare di come migliorare questa capacità?",
                "La solidarietà è una forza potente che può unire le persone. Hai esperienze recenti in cui hai sentito una forte empatia verso qualcuno?",
                "L'empatia non solo aiuta gli altri, ma arricchisce anche noi stessi. Ti sei mai sentito meglio dopo aver mostrato comprensione verso qualcuno?",
                "Condividere i sentimenti con gli altri può essere profondamente liberatorio. Hai avuto occasione di condividere recentemente?"
                };
            }
            else if (explorationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Esplorare il mondo può essere un'avventura entusiasmante. Hai già in mente una destinazione?",
                "Ogni viaggio inizia con un passo. Dove ti porterà la tua prossima esplorazione?",
                "Scoprire nuovi luoghi e culture può arricchire la mente e lo spirito. Qual è la tua prossima fermata?",
                "Viaggiare apre nuovi orizzonti e nuove prospettive. Che esperienze cerchi nel tuo prossimo viaggio?",
                "L'esplorazione non è solo geografica, ma anche interiore. Quali nuovi interessi stai cercando di esplorare?"
                };
            }

            else if (exhaustionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi stanchi può segnalare che è tempo di prendersi una pausa. Hai considerato di riposare un po'?",
                "Il riposo è fondamentale quanto il lavoro. Assicurati di dare al tuo corpo e alla tua mente il tempo di ricaricarsi.",
                "A volte, quando siamo esausti, il miglior rimedio è una buona notte di sonno. Hai dormito bene ultimamente?",
                "Prendersi cura di sé è cruciale, specialmente quando ci si sente a terra. Quali attività rilassanti potrebbero aiutarti a rigenerarti?",
                "Ricorda che è umano sentirsi sfiniti. Non esitare a chiedere aiuto se il carico è troppo pesante."
                };
            }
            else if (curiosityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La curiosità è il motore dell'apprendimento e dell'innovazione. Cosa ti piacerebbe esplorare oggi?",
                "È fantastico vedere il tuo interesse! Hai delle domande specifiche o un argomento che ti interessa particolarmente?",
                "La curiosità ti porta a nuovi orizzonti. Esplora liberamente e goditi ogni nuova scoperta!",
                "Ricorda che ogni grande scoperta inizia con una semplice domanda. Qual è la tua domanda oggi?",
                "La curiosità è un meraviglioso punto di partenza per qualsiasi avventura di apprendimento. Continua a chiedere, continuare a imparare!"
                };
            }
            else if (lossKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                    "La perdita può essere estremamente dolorosa. Cerca conforto nel sostegno degli amici e dei familiari.",
                    "Prenditi il ​​tempo di elaborare i tuoi sentimenti di dolore e lutto. Non c'è una scadenza per il processo di guarigione.",
                    "Ricorda che non sei solo nella tua esperienza di perdita. Trova conforto nella condivisione delle tue esperienze con gli altri.",
                    "Onora il ricordo del tuo caro attraverso gesti significativi e rituali che celebrano la loro vita.",
                    "Cerca professionisti o gruppi di supporto che possano offrire sostegno e risorse durante il periodo di lutto."
                };
            }
            else if (socialPressureKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                    "La pressione sociale può essere opprimente, ma ricorda che sei libero di seguire il tuo percorso unico nella vita.",
                    "Focalizzati su ciò che è importante per te, piuttosto che sull'approvazione degli altri. Cosa ti rende veramente felice?",
                    "Impara a riconoscere quando la pressione sociale influisce sulle tue decisioni e cerca di prendere decisioni autentiche.",
                    "Circondati di persone che ti sostengono e accettano per quello che sei, senza giudizio o aspettative irrealistiche.",
                    "Concentrati sul tuo progresso personale anziché confrontarti con gli altri. La tua strada è unica e preziosa.",
                    "L'ansia da prestazione è comune, ma ci sono modi per gestirla. Cerca tecniche di rilassamento che funzionino per te.",
                    "Spezza i compiti impegnativi in passaggi più piccoli e gestibili per ridurre la sensazione di sopraffazione.",
                    "Visualizza il successo invece di concentrarti sul fallimento. Immagina il risultato desiderato e concentrati su come arrivarci.",
                    "Pratica la respirazione profonda e consapevole per calmare i nervi e ridurre lo stress durante situazioni ansiose.",
                    "Ricorda che tutti sperimentano l'ansia da prestazione. È importante essere gentili con te stesso e accettare che non siamo perfetti."
                };
            }
            else if (selfCriticismKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                    "Essere troppo critici con se stessi può essere dannoso. Prova a trattarti con la stessa gentilezza che useresti con un amico.",
                    "Riconoscere i tuoi successi, anche quelli piccoli, può aiutare a bilanciare l'autocritica. Quali sono alcuni successi recenti di cui puoi essere orgoglioso?",
                    "Parlare delle tue insicurezze può aiutare a mettere le cose in prospettiva. Vuoi discutere di ciò che ti fa sentire critico verso te stesso?",
                    "Impostare aspettative realistiche e raggiungibili può ridurre la pressione che ti metti addosso. Che obiettivi realistici potresti fissare?",
                    "Considera di esplorare tecniche di mindfulness per essere più presente e meno giudicante verso te stesso "
                };
            }

            else if (rejectionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Affrontare il rifiuto o il fallimento è difficile, ma è un'esperienza comune che tutti noi attraversiamo. Ricorda che non definisce chi sei.",
                "Ogni fallimento è un'opportunità per imparare e crescere. C'è qualcosa che puoi trarre da questa esperienza per il futuro?",
                "Condividere i tuoi sentimenti di delusione può essere molto catartico. Hai qualcuno di fiducia con cui parlare?",
                "Riflettere su ciò che hai imparato e su come puoi migliorare per la prossima volta può trasformare il rifiuto in un trampolino di lancio.",
                "Darsi il permesso di sentirsi male è ok, ma non lasciare che ti trascini giù. Quali sono i passi che puoi fare per rialzarti?"
                };
            }
            else if (decisionOverwhelmKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Affrontare decisioni difficili può sembrare scoraggiante. Prova a suddividere le decisioni in parti più piccole e gestibili. Che ne pensi?",
                "Ricorda che non devi prendere decisioni da solo. Parlare con amici, familiari o consulenti può offrire nuove prospettive.",
                "Scrivere pro e contro può aiutare a chiarire i tuoi pensieri. Vuoi che ti guidi attraverso questo processo?",
                "A volte, prendersi una pausa dai problemi decisionali può aiutare a schiarire la mente. Hai provato a distogliere la mente dalle decisioni per un po'?",
                "Considera l'importanza di ogni decisione. Alcune scelte potrebbero non avere un impatto così grande come sembra. Aiuta a ridimensionare le cose?"
                };
            }
            else if (uncertaintyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'incertezza riguardo al futuro può essere angosciante, ma è anche una parte normale della vita. Hai considerato di esplorare diverse opzioni e percorsi?",
                "Focalizzarsi su ciò che puoi controllare, come le tue reazioni e le tue azioni quotidiane, può aiutare a gestire l'ansia per il futuro.",
                "Parlare dei tuoi timori e delle tue speranze per il futuro con qualcuno può rendere le cose meno intimidatorie. Vuoi iniziare ora?",
                "A volte, scrivere i tuoi pensieri e piani futuri può aiutare a chiarire la tua mente e a ridurre l'ansia.",
                "Considera di stabilire obiettivi a breve termine che ti motivino e ti diano qualcosa di concreto su cui lavorare, anziché preoccuparti per il lungo termine."
                };
            }
            else if (stuckKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi bloccati può essere frustrante, ma spesso è un segno che è il momento di fare una valutazione della propria vita. Vuoi aiuto per iniziare?",
                "A volte, cambiare la routine quotidiana può aiutare a sentirsi meno bloccati. Hai considerato di aggiungere o modificare qualche abitudine?",
                "Parlare con un coach di vita o un terapeuta può offrire nuove prospettive e strumenti per aiutarti a muoverti in avanti.",
                "Impostare piccoli obiettivi raggiungibili può aiutare a creare un senso di progresso. Che piccolo passo potresti fare oggi?",
                "A volte, prendersi una pausa e poi tornare a guardare la propria situazione può rivelare soluzioni non evidenti prima."
                };
            }
            else if (disappointmentKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Affrontare la delusione è duro, ma è importante ricordare che non definisce il tuo futuro. Vuoi parlare di cosa è andato storto?",
                "A volte, rivedere le nostre aspettative può aiutare a gestire meglio le delusioni future. Hai considerato di impostare obiettivi più realistici?",
                "Condividere i tuoi sentimenti con qualcuno di fiducia può aiutare a elaborare la delusione. C'è qualcuno con cui ti senti a tuo agio a parlare?",
                "Ricorda che ogni delusione porta con sé un'opportunità di apprendimento. Quali lezioni puoi trarre da questa esperienza?",
                "Darsi il tempo di guarire è importante. Assicurati di prenderti cura di te stesso mentre lavori attraverso questi sentimenti."
                };
            }
            else if (socialOverwhelmKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Avere un calendario troppo pieno può essere stressante. Potrebbe essere utile rivedere i tuoi impegni e vedere se puoi dire di no a qualcosa.",
                "Ricorda l'importanza di prendere tempo per te stesso. È perfettamente accettabile avere bisogno di spazio per ricaricare.",
                "A volte, dobbiamo imparare a priorizzare i nostri impegni. Quali sono le attività o gli eventi che ti danno più gioia o valore?",
                "Parlare con un organizzatore di eventi o un coach di vita può aiutare a gestire meglio il tuo tempo e i tuoi impegni sociali.",
                "Considera l'uso di strumenti di gestione del tempo, come le app di calendario, per aiutarti a tenere traccia e bilanciare i tuoi impegni."
                };
            }
            else if (judgmentFearKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La paura del giudizio è comune, ma ricordati che la maggior parte delle persone è più concentrata su se stessa che sugli altri.",
                "Considera di lavorare sul rafforzamento della tua autostima. Sentirsi sicuri di sé può ridurre la paura di come gli altri ci percepiscono.",
                "Parlare con un terapeista potrebbe aiutarti a superare queste paure. Vuoi che ti aiuti a trovare un professionista qualificato?",
                "Imparare a sentirsi a proprio agio con se stessi è un processo. Che ne dici di fare piccoli passi praticando situazioni sociali in ambienti sicuri?",
                "Ricorda, non è necessario piacere a tutti. Concentrati sulle opinioni di persone che realmente contano per te."
                };
            }
            else if (irritabilityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi irritabile può essere un segnale che hai bisogno di più riposo o tempo per te. Hai considerato di prenderti una pausa?",
                "A volte l'irritabilità può essere causata da stress o stanchezza. Che ne dici di esaminare cosa potrebbe essere alla base di questi sentimenti?",
                "Parlare delle cose che ti irritano può aiutare a diminuire la tensione. Vuoi condividere cosa ti ha fatto sentire così?",
                "Praticare la mindfulness o la meditazione può aiutare a gestire meglio l'irritabilità. Ti interessa saperne di più su queste tecniche?",
                "Assicurarti di mangiare bene e idratarti adeguatamente può influenzare positivamente il tuo umore. Hai mangiato qualcosa di recente?"
                };
            }
            else if (worthlessnessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi inutili può essere profondamente doloroso, ma è importante ricordare che il tuo valore non è definito solo dalle tue prestazioni.",
                "Considera di dedicarti a attività che trovi significative o gratificanti, anche qualcosa di piccolo può fare una grande differenza nella percezione di sé.",
                "Parlare di questi sentimenti con qualcuno che capisce e supporta può essere molto liberatorio. Hai qualcuno con cui puoi parlare?",
                "Aiutare gli altri può essere un modo efficace per sentirsi più utili. Hai considerato il volontariato o offrire supporto a chi ne ha bisogno?",
                "A volte, tenere un diario delle gratitudini può aiutare a riconoscere e apprezzare i propri contributi e successi, grandi o piccoli."
                };
            }
            else if (trustIssuesKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La fiducia si costruisce con il tempo e la comunicazione aperta. Hai provato a esprimere apertamente i tuoi sentimenti alle persone coinvolte?",
                "Esplorare le radici della tua mancanza di fiducia può essere utile. A volte, parlare con un professionista può offrire intuizioni preziose.",
                "Impostare piccoli 'test' di fiducia può aiutare a ricostruire la fiducia gradatamente. Quali piccoli passi potresti considerare per iniziare?",
                "Ricorda che la fiducia richiede reciprocità. Condividere esperienze positive e feedback può rafforzare le relazioni.",
                "Leggere libri o seguire corsi su come costruire relazioni sane potrebbe darti strumenti utili per migliorare la fiducia nelle interazioni."
                };
            }
            else if (overloadKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi sovraccaricati può essere estenuante. Prova a delegare alcune responsabilità o a posticipare ciò che non è urgente.",
                "Ricorda l'importanza di fare pause regolari. Darsi spazio per respirare può migliorare significativamente la gestione dello stress.",
                "Organizzare e priorizzare le tue attività può aiutarti a gestire meglio il carico di lavoro. Hai provato a usare strumenti di pianificazione?",
                "È importante riconoscere i propri limiti e comunicarli chiaramente agli altri. Hai parlato con qualcuno del tuo carico di lavoro?",
                "A volte, riconsiderare e rinegoziare le scadenze può alleviare la pressione. Considera di discutere le tue scadenze con chi di dovere."
                };
            }
            else if (changeFearKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Affrontare il cambiamento può essere intimidatorio, ma ricordati che il cambiamento può portare a nuove opportunità e crescita personale.",
                "Parlare delle tue paure con qualcuno può aiutarti a decomprimere e forse a vedere il cambiamento sotto una nuova luce.",
                "Prova a fare piccoli passi verso il cambiamento. Familiarizzare gradualmente con il nuovo può renderlo meno spaventoso.",
                "Visualizzare i possibili risultati positivi del cambiamento può aiutare a mitigare la paura. Hai provato a immaginare i potenziali benefici?",
                "Considera l'aiuto di un coach o di un terapeuta per navigare attraverso le tue paure. A volte, un supporto esterno può fare una grande differenza."
                };
            }
            else if (criticismKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Ricevere critiche può essere difficile. È importante valutare se la critica è costruttiva e come può essere utilizzata per crescere.",
                "Ricordati di non prendere la critica sul personale. Spesso, le osservazioni negative riguardano comportamenti specifici, non la tua persona.",
                "Confrontarsi con la critica apertamente può trasformarla in un'opportunità di apprendimento. Hai considerato di chiedere chiarimenti per capire meglio?",
                "È utile ricordare che tutti riceviamo critiche. L'importante è come rispondiamo e cosa scegliamo di farne.",
                "Mantenere una prospettiva positiva e ricordare i tuoi successi può aiutarti a gestire meglio il feedback negativo."
                };
            }
            else if (jealousyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La gelosia è un'emozione comune ma può essere difficile da gestire. Hai provato a riflettere sulle origini di questi sentimenti?",
                "Parlare apertamente dei tuoi sentimenti di gelosia con una persona di fiducia può aiutare a vederli sotto una nuova luce e trovare soluzioni.",
                "Ricordati che la gelosia spesso nasce dall'insicurezza. Lavorare sull'autostima potrebbe aiutarti a sentirsi più sicuro nelle tue relazioni.",
                "Concentrarsi sulle proprie qualità e successi può aiutare a mitigare i sentimenti di invidia o gelosia verso gli altri.",
                "Considera di esplorare queste emozioni con l'aiuto di un professionista, come un terapeuta, che può offrire strumenti e tecniche per gestirle meglio."
                };
            }
            else if (boredomKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La noia può essere un'opportunità per esplorare nuovi interessi. Hai pensato a intraprendere un nuovo hobby?",
                "Organizzare un incontro con amici o partecipare a eventi locali può essere un ottimo modo per rompere la monotonia.",
                "A volte, anche rilassarsi e non fare nulla è importante. Ti sei concesso tempo per riposarti ultimamente?",
                "Volontariato può essere un modo gratificante per impiegare il tempo libero. Hai esplorato questa possibilità?",
                "Imparare qualcosa di nuovo, come una lingua o uno strumento musicale, può essere stimolante e allontanare la noia."
                };
            }
            else if (changeManagementKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Affrontare i cambiamenti può essere intimidatorio ma anche eccitante. Cosa ti preoccupa di più di questo cambiamento?",
                "Ricorda che adattarsi a nuove situazioni è un processo. Datti tempo per ambientarti.",
                "Parlare dei cambiamenti con amici o familiari può darti una nuova prospettiva o idee su come gestirli al meglio.",
                "Stabilire una routine quotidiana può aiutare a gestire meglio il cambiamento. Hai considerato di organizzare la tua giornata in modo più strutturato?",
                "Cerca di concentrarti sugli aspetti positivi del cambiamento. Quali nuove opportunità ti porterà questa transizione?"
                };
            }
            else if (performanceAnxietyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "L'ansia da prestazione è comune, ma ci sono strategie che possono aiutare. Hai provato a visualizzare il successo prima dell'evento?",
                "Respirazione profonda e meditazione possono essere molto utili prima di un evento stressante. Vuoi che ti guidi attraverso un esercizio di respirazione?",
                "Parlare delle tue preoccupazioni con un coach o un mentore può aiutarti a mettere in prospettiva le tue paure.",
                "Preparazione adeguata e pratica sono chiavi per superare l'ansia da prestazione. Ti senti ben preparato?",
                "Ricorda che è normale essere nervosi prima di un grande evento. Accettare i tuoi sentimenti può aiutare a ridurne l'impatto."
                };
            }
            else if (frustrationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi frustrati è normale, ma trovare modi sani per gestire quella frustrazione è chiave. Hai provato a esprimere i tuoi sentimenti attraverso lo sport o l'arte?",
                "Parlare delle fonti della tua frustrazione può aiutare a chiarirle e, forse, a risolverle. Vuoi discuterne?",
                "A volte, prendere una pausa dalle situazioni che causano frustrazione può aiutare a vedere le cose da una prospettiva diversa.",
                "Stabilire obiettivi realistici e raggiungibili può ridurre la frustrazione. Hai obiettivi chiari impostati per te stesso?",
                "Ricordati di prendere respiro profondo quando ti senti frustrato. Aiuta a calmare la mente e il corpo."
                };
            }
            else if (lonelinessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi soli può essere molto difficile. A volte, partecipare a eventi comunitari o gruppi può aiutare a sentirsi più connessi.",
                "Considera l'adozione di un animale domestico se le circostanze lo permettono. Gli animali possono offrire grande compagnia.",
                "Creare connessioni online attraverso forum o social media può essere un modo per sentirsi meno isolati.",
                "Volontariato può essere un ottimo modo per incontrare persone e sentirsi utili. Hai considerato questa possibilità?",
                "Ricordati che non sei solo a sentirti solo. Molti passano per questo e parlare apertamente può aiutare."
                };
            }
            else if (sadnessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi tristi è una parte normale della vita, ma importante è come gestiamo questa emozione. Hai voglia di parlare di cosa ti rende triste?",
                "A volte, fare attività fisica può aiutare a migliorare l'umore. Hai provato a fare una passeggiata o esercizio fisico?",
                "Ricordati che è okay chiedere aiuto. Parlare con un amico o un professionista può offrire sostegno.",
                "Cerchi di mantenere una routine quotidiana? A volte, la struttura può aiutare a gestire la tristezza.",
                "L'arte e la musica possono essere ottimi sfoghi per i sentimenti di tristezza. Hai qualche hobby creativo che ti aiuta?",
                "È normale sentirsi tristi a volte. Parlane con qualcuno, può aiutarti a sentirti meglio.",
                "Concentrati su piccole attività che ti danno gioia, anche durante i momenti di tristezza.",
                "La tristezza può essere un segnale che è il momento di rallentare e prendersi cura di sé stessi. Cosa ti fa sentire meglio?",
                "Esprimere i tuoi sentimenti attraverso l'arte, la scrittura o la musica può essere un modo terapeutico per affrontare la tristezza.",
                "Ricorda che la tristezza è temporanea. Cerca di mantenere la prospettiva e prenditi il tempo di guarire."
                };
            }
            else if (stressKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Gestire lo stress è fondamentale per la tua salute mentale e fisica. Hai provato tecniche di rilassamento come la meditazione o lo yoga?",
                "Parlare delle cause dello stress può aiutare a trovare soluzioni. Vuoi discutere di ciò che ti sta mettendo sotto pressione?",
                "A volte, prendere una pausa, anche solo per pochi minuti, può aiutare a ridurre lo stress significativamente.",
                "Assicurati di non trascurare il sonno e l'alimentazione, che sono fondamentali per affrontare i periodi di stress.",
                "Imparare a dire no è importante. Non sovraccaricarti di impegni se ti senti già stressato."
                };
            }
            else if (inadequacyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi inadeguati è qualcosa che molti di noi provano, ma è importante ricordare che sei unico e hai molto da offrire.",
                "Confrontarsi con gli altri può spesso farci sentire inadeguati. Cerca di concentrarti sui tuoi progressi personali e sulle tue vittorie.",
                "Riflettere sui tuoi successi passati può aiutarti a combattere i sentimenti di inadeguatezza. Cosa hai realizzato di cui sei orgoglioso?",
                "Parlare dei tuoi sentimenti può aiutare. Considera di discutere delle tue insicurezze con un amico fidato o un terapeuta.",
                "Impegnarsi in attività che ti piacciono e in cui eccelli può aumentare la tua autostima e ridurre i sentimenti di inadeguatezza."
                };
            }
            else if (lowMotivationKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "A volte, la mancanza di motivazione può essere un segno che abbiamo bisogno di una pausa. Hai considerato di prenderti un momento per rilassarti?",
                "Impostare piccoli obiettivi quotidiani può aiutare a riaccendere la tua motivazione. Qual è una piccola cosa che potresti fare oggi?",
                "Parlare delle tue sfide può aiutarti a ritrovare la motivazione. C'è qualcuno con cui puoi discutere dei tuoi obiettivi?",
                "Ricorda che la motivazione fluttua. Non essere troppo duro con te stesso durante i periodi di bassa energia.",
                "A volte, cambiare l'ambiente o routine può stimolare la motivazione. Hai provato a cambiare il tuo spazio di lavoro o la tua routine quotidiana?"
                };
            }
            else if (futureAnxietyKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Preoccuparsi per il futuro è comune, ma ricorda, molte preoccupazioni non si avverano mai. Concentrati sul vivere il momento presente.",
                "Avere un piano può aiutare a ridurre l'ansia per il futuro. Hai già pensato a stabilire degli obiettivi a breve termine?",
                "Parlare delle tue preoccupazioni può alleggerire il peso. Vuoi condividere ciò che ti preoccupa di più del futuro?",
                "Praticare la mindfulness può aiutarti a sentirti più radicato e meno ansioso. Hai mai provato tecniche di mindfulness o meditazione?",
                "Ricorda che il futuro è un foglio bianco, e tu hai il potere di influenzarne il corso con le tue azioni quotidiane."
                };
            }
            else if (insecurityKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi insicuri è qualcosa che tutti proviamo in un momento o nell'altro. È importante ricordare i tuoi successi e i tuoi punti di forza.",
                "Confrontarsi con gli altri può spesso alimentare insicurezze. Cerca di concentrarti sui tuoi progressi personali, non sul confronto con gli altri.",
                "Praticare l'affermazione di sé può essere un buon modo per migliorare la tua autostima. Hai provato a dire ad alta voce ciò che apprezzi di te stesso?",
                "Chiedere feedback positivi da persone di fiducia può aiutare a vedere una prospettiva diversa su di te.",
                "Considera l'idea di lavorare con un terapeuta o un coach per costruire la tua autostima. A volte, un aiuto professionale può fare la differenza."
                };
            }
            else if (griefKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Affrontare un lutto è un processo molto personale e ci vuole tempo per guarire. È importante permettersi di provare dolore.",
                "Parlare dei propri sentimenti può essere molto terapeutico in tempi di lutto. Hai qualcuno con cui puoi parlare liberamente?",
                "A volte, unirsi a gruppi di supporto dove altre persone condividono esperienze simili può offrire conforto e comprensione.",
                "Ricorda di prenderti cura di te stesso, anche durante il lutto. Mangiare bene, dormire a sufficienza e fare attività fisica sono fondamentali.",
                "Scrivere i propri pensieri e sentimenti in un diario può aiutare a elaborare la perdita."
                };
            }
            else if (workStressKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Lo stress lavorativo è comune, ma importante da gestire. Hai considerato stabilire confini più chiari tra lavoro e tempo libero?",
                "A volte, parlare con il proprio capo delle proprie preoccupazioni può aiutare a ridurre la pressione. Pensi sia un'opzione per te?",
                "Organizzare il lavoro in modo più efficiente, magari con l'uso di liste o priorità, può alleviare il senso di sovraccarico.",
                "Prendere regolarmente brevi pause durante il lavoro può aiutare a mantenere la mente fresca e ridurre lo stress.",
                "Considerare attività rilassanti dopo il lavoro, come lo yoga o la meditazione, può aiutare a distaccarsi dalle tensioni lavorative."
                };
            }
            else if (exclusionKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi esclusi può fare molto male. È importante circondarsi di persone che ti apprezzano veramente.",
                "Essere ignorati dagli altri può essere doloroso. Hai considerato unirti a nuovi gruppi o attività dove potresti sentirte più benvenuto?",
                "A volte, parlare apertamente dei nostri sentimenti con le persone coinvolte può aiutare a risolvere situazioni di esclusione.",
                "Ricorda che il tuo valore non dipende dall'approvazione degli altri. Hai qualità uniche che ti rendono speciale.",
                "Se ti senti solo, esplorare nuovi hobby o interessi può essere un buon modo per incontrare nuove persone con passioni simili."
                };
            }
            else if (failureKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Sentirsi falliti è umano, ma ricorda che ogni esperienza è un'opportunità di crescita.",
                "Il fallimento è solo temporaneo; l'importante è rialzarsi e continuare a provare. Cosa hai imparato da questa esperienza?",
                "Ogni persona fallisce a un certo punto. L'importante è continuare a lottare per i tuoi obiettivi.",
                "A volte, rivedere i nostri obiettivi e aspettative può aiutarci a superare il senso di fallimento. Vuoi ripensare ai tuoi obiettivi insieme?",
                "Parlare dei propri fallimenti può essere molto liberatorio. Se vuoi, sono qui per ascoltarti."
                };
            }
            else if (angerKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La rabbia è un'emozione forte, ma è importante gestirla in modo sano. Hai provato tecniche di rilassamento?",
                "Esprimere la rabbia in modo costruttivo può aiutare a prevenire che si trasformi in qualcosa di più nocivo. Vuoi parlare di cosa ti ha fatto arrabbiare?",
                "A volte, fare una passeggiata o praticare esercizi fisici può aiutare a calmare la mente quando sei arrabbiato.",
                "Parlare con qualcuno di fiducia può essere un ottimo modo per alleviare la rabbia. C'è qualcuno con cui puoi condividere i tuoi sentimenti?",
                "Scrivere ciò che ti fa arrabbiare può anche aiutare a gestire meglio queste emozioni. Hai mai provato a tenere un diario emotivo?"
                };
            }
            else if (botWellbeingKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Io sto sempre bene, grazie! E tu come stai?",
                "Sono un bot, quindi non ho sentimenti, ma grazie per aver chiesto! E tu come stai?",
                "Non provo emozioni, ma sono qui per te! Come posso aiutarti oggi?"
                };
            }
            else if (entertainmentKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Parlare di film e serie è sempre divertente! Hai qualche preferenza di genere?",
                "I film sono un ottimo modo per rilassarsi. Qual è il tuo film preferito?",
                "C'è una serie che ti ha particolarmente colpito di recente?",
                "Il tempo libero è perfetto per esplorare nuovi hobby. Cosa ti piace fare nel tuo tempo libero?",
                "Avere un hobby può arricchire la tua vita. Qual è il tuo hobby preferito?"
                };
            }
            else if (relationshipKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Le relazioni sono complesse ma molto gratificanti. Hai qualcuno con cui condividere i tuoi pensieri?",
                "L'amore e l'amicizia sono fondamentali nella vita di tutti. Stai vivendo un bel periodo a riguardo?",
                "Gestire le relazioni può essere impegnativo. Vuoi parlare di qualcosa in particolare?",
                "È importante avere qualcuno con cui parlare, sia amici che famiglia. Ti senti supportato dalle persone intorno a te?",
                "Avere un partner con cui condividere la vita può essere molto bello. Come va la tua vita amorosa?"
                };
            }
            else if (happinessKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "La felicità è una scelta che facciamo ogni giorno, continua a sorridere!",
                "È bello sentire che sei felice! Che cosa ha contribuito a questo?",
                "La felicità è contagiosa! Passala a chi ti sta intorno.",
                "La gioia di vivere spesso deriva dalle piccole cose. Qual è stata la tua piccola gioia oggi?",
                "Ricorda, la felicità cresce quando la condividi."
                };
            }
            else if (learningKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Imparare qualcosa di nuovo ogni giorno ci tiene giovani e agili mentalmente!",
                "L'apprendimento è un viaggio senza fine. Di cosa sei curioso oggi?",
                "Non ci sono limiti a ciò che puoi imparare, eccetto i limiti che ti poni.",
                "Ricorda, ogni esperto era una volta un principiante.",
                "Che bello! L'apprendimento può cambiare la vita in meglio. Continua così!"
                };
            }
            else if (challengeKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Le sfide ci rendono più forti. Affronta la tua con coraggio!",
                "Ogni grande sfida è un'opportunità per crescere e imparare.",
                "Non arrenderti di fronte a una sfida, usa la tua creatività per superarla!",
                "Le sfide della vita ci insegnano a essere resilienti. Hai tutto ciò che ti serve per superarle.",
                "Affrontare una sfida può essere duro, ma superarla è incredibilmente gratificante."
                };
            }
            else if (healthKeywords.Any(k => message.Contains(k)))
            {
                responses = new string[]
                {
                "Prendersi cura della propria salute è il primo passo verso una lunga e felice vita.",
                "La salute non è solo assenza di malattia, ma un completo benessere fisico, mentale e sociale.",
                "Ricorda di fare scelte sane ogni giorno; il tuo corpo ti ringrazierà!",
                "La salute è la vera ricchezza. Investi in essa!",
                "Mantenere una buona salute richiede tempo e impegno, ma i benefici ne valgono la pena."
                };
            }
            else
            {
                responses = new string[] { "Non sono sicuro di come rispondere a questo. Potresti dirmi di più?" };
            }



            string selectedResponse;

            if (responses.Length > 1)
            {
                do
                {
                    selectedResponse = responses[random.Next(responses.Length)];
                } 
                while (selectedResponse == lastResponse);
            }
            else
            {
                selectedResponse = responses.Length > 0 ? responses[0] : "Nessuna risposta disponibile";
            }

            lastResponse = selectedResponse;

            return Json(new { Response = selectedResponse }, JsonRequestBehavior.AllowGet);

        }
    }
}