Whisper è un'applicazione social sviluppata utilizzando il framework Entity Framework e seguendo il pattern MVC (Model-View-Controller). 
Questa piattaforma è progettata per fornire agli utenti un ambiente interattivo dove possono postare contenuti, commentare, mettere "like" e gestire il proprio profilo e le interazioni sociali,
tutto questo mantenendo l'anonimato. 

Esistono due interfacce principali: Utente e Admin.

Caratteristiche

 - Lato Admin:
1) Gestione delle segnalazioni: Gli admin possono visualizzare e gestire le segnalazioni fatte dagli utenti riguardanti commenti, post e messaggi di chat, e decide se in caso
di bannare un utente.

2) Gestione degli aforismi: Possibilità di aggiungere, modificare o eliminare aforismi sulla crescità personale, che veranno mostrati agli utenti
in modo Randomico ogni 15secondi.

3) Gestione degli avatar: Gli admin possono eliminare o aggiungere nuovi avatar.

4) Gestione delle pubblicità: Inserimento, modifica o cancellazione delle pubblicità visualizzate sulla piattaforma, nella Home page che veranno mostrate
agli utenti in modo randomico ogni 30secondi.


 - Lato Utente:
1) Registrazione e accesso: Gli utenti possono registrarsi rispettando fattori come Username già non esistente nè uguale al nome appena digitato nè ad altri nomi presenti nel DB
e accedere controllando email e password.

2) Interfaccia principale: Dopo il login, l'utente accede alla home page dove può:
A) Scrivere e gestire i propri post (inserire, modificare o cancellare).
B) Segnalare nel caso di altri utenti post e commenti.
C) Commentare e mettere "like" ai post.
D) Iniziare una nuova chat con un altro utente.

4) Visualizzare i profili altrui per vedere tutti i post pubblicati da questi utenti con la possibilità di seguirli ( " origliare " stile Follow ) o di iniziare una chat privata.
   
5) Entrare nel proprio profilo per visualizzare le amicizie e i propri post, e avere la possibilità di modificare o disattivare il proprio profilo.
   
6) Notifiche: Le notifiche sono organizzate in tre categorie:
A) Notifiche di post, commenti e "like" sotto l'icona della campanella.
B) Notifiche di amicizie ricevute sotto l'icona dei due omini.
C) Notifiche di chat e messaggi sotto l'icona del fumetto.

 - Tecnologie Utilizzate:
Entity Framework: Utilizzato per il mapping e la gestione della base dati relazionale.
MVC Pattern: Model per la logica dei dati, View per l'interfaccia utente e Controller per il controllo dell'applicazione.

- Database dell'applicazione presente nella repository https://github.com/Lorenzodenny/Database-Whisper
