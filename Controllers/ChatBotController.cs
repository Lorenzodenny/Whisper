using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Whisper.Controllers
{
    public class ChatBotController : Controller
    {
        private static readonly Random random = new Random();

        public ActionResult GetBotResponse(string message)
        {
            string[] responses;

            // Uso di un array di parole chiave per identificare la categoria di risposta
            string[] successKeywords = new string[] { "successo", "riuscita", "vittoria", "conquista", "trionfo" };
            string[] happinessKeywords = new string[] { "felice", "felicità", "gioia", "contento", "beato" };
            string[] learningKeywords = new string[] { "imparare", "apprendimento", "studio", "educazione", "conoscenza" };
            string[] challengeKeywords = new string[] { "sfida", "difficoltà", "ostacolo", "prova", "compito" };
            string[] healthKeywords = new string[] { "salute", "benessere", "forma", "fitness", "sanità" };
            string[] botWellbeingKeywords = new string[] { "come stai", "tutto bene", "stai bene", "come va", "va tutto bene" };
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

            // Ottiene una risposta casuale dall'array di risposte
            string selectedResponse = responses[random.Next(responses.Length)];

            return Json(new { Response = selectedResponse }, JsonRequestBehavior.AllowGet);
        }
    }
}