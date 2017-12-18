IPROG - Projet 2017 :

La bataille navale “salvo”

L’objectif de ce projet est la création d’un jeu de bataille navale en C#.

# Règles du jeu

La bataille navale trouve son origine dans un jeu français appelé l'attaque qui a des liens de parenté étroits avec le jeu de M. Horseman la baslinda qui vit le jour en 1890. 

Le but de la bataille navale est de couler tous les bateaux de son adversaire. Dans un premier temps, chaque joueur place ses bateaux sur le plateau sans voir ceux de son adversaire. Ensuite, chacun à leur tour, les joueurs essaient de trouver et de couler les bateaux de l'adversaire.

Le plateau de jeu est composé de 10 colonnes ("A" à "J") et 10 lignes ("1" à "10"). Voici une liste des navires et le nombre de cases qu'ils occupent :

- Le porte-avions est le navire le plus long et il occupe 5 cases sur le plateau de jeu.
- Le cuirassé est plus petit que le porte-avions, il occupe 4 cases sur votre plateau de jeu.
- Le sous-marin et le croiseur sont un peu plus petits, ils occupent tous les deux 3 cases.
- Le contre-torpilleur est le navire le plus petit (et donc le plus difficile à couler). Il occupe 2 cases sur le plateau de jeu.

![img](https://lh6.googleusercontent.com/YVKVGRKEoBFGICT3Uv72bufW7dgRgSRt69lxUKUeUd95fvXXDKZFD3KJFAtP0a3h4KBABiRZKU4x4gNXOiQBaFKTDRPTVthk97QaQASCDTpSq-lvq3Iikf5tDIvlFWY3jG-zAUWl)

Une fois les bateaux positionnés, la partie commence. A chaque tir annoncé par un joueur (par exemple C5), le second joueur regarde si l’un de ses navires occupe la case visée. Il doit répondre afin de dire à l'autre joueur s'il a touché ou coulé un navire ou bien tiré dans l'eau. Le premier joueur qui coule tous les bateaux de son adversaire gagne la partie.

Dans la variante “salvo” de la bataille navale, chaque joueur annonce plusieurs tirs à la fois. Le nombre de tirs simultanés est de 5 initialement. Ce nombre diminue en fonction du nombre de bateaux déjà coulés. Quand un joueur détruit un navire de son adversaire, il ne tire plus que 4 coups au lieu de 5. Quand 2 bateaux sont détruits, le joueur ennemi tire uniquement 3 coups. Quand 3 navires sont détruits, le joueur opposé ne tire plus que 2 coups, etc. 

# Fonctionnalités

Le programme réalisé permet à un joueur humain d’affronter l’ordinateur. Il affiche deux grilles :

- Celle des bateaux du joueur, avec les coups au but de l’adversaire et les bateaux du déjà détruits.
- Celle des bateaux de l’adversaire, avec les coups au but du joueur et les bateaux déjà détruits.

Le placement des bateaux du joueur et de l’ordinateur en début de partie sont aléatoires. Le joueur a la possibilité de visualiser le placement obtenu et d’en demander un autre jusqu’à ce que le résultat lui convienne.

La saisie des coups du joueur est contrôlée afin d’éviter toute erreur. De manière générale, l’ergonomie du programme (saisies, affichages, déroulement de la partie) doit être étudiée avec attention.

Au niveau de difficulté minimal, l’ordinateur tire au hasard sur les bateaux du joueur. Un niveau supplémentaire optionnel le fait jouer plus “intelligemment”, par exemple en essayant de couler les bateaux touchés.

Lors de son tour, le joueur aura la possibilité de sauvegarder une partie qu’il pourra charger ultérieurement. Les positions des bateaux et l’état de la partie seront stockées dans un fichier texte selon une organisation à définir.

# Contraintes techniques

La programmation est faite en C#, en mode console, sous Windows, dans les conditions habituelles des TPs, sans utilisation de bibliothèques de fonctions externes (autre que les bibliothèques standards du framework .NET évidemment).

La programmation orientée objets est exclue (à part les habituels appels à des méthodes systèmes utilisées en TP telles que Console.WriteLine),

L’utilisation des collections est exclue (listes, tables de hachages…), le traitement sera basé sur des manipulations de tableaux,

Le code source doit répondre aux règles de programmation et conventions de nommage suivantes :

- Indentation partout correcte.
- Commentaires pour expliquer les parties complexes et/ou importantes du code.
- Respect de la norme [camelCase](https://fr.wikipedia.org/wiki/Camel_case).
- Nommage des sous-programmes à l’aide d’un verbe à l’infinitif et en commençant par une majuscule (exemple : CalculerRésultat).

# Travail à rendre

Le travail est effectué en binôme ou seul, le nombre total de groupe étant minimisé (pas de monôme pour convenance personnelle),

Vous devez fournir pour le 18 janvier 2017 dernier délai les livrables suivants :

- La solution nettoyée de votre projet (fichier .sln ainsi que l’ensemble des fichiers .cs et .csproj associés, pas de fichiers objets, pas d’exécutable).
- Un document PDF de justifications techniques expliquant la structure du code source fourni.
- Une documentation utilisateur en PDF illustrant l'ensemble du fonctionnement du programme développé, avec exemples de déroulement et description du format de sauvegarde d’une partie.

Ces livrables doivent être contenus dans une archive qui s’extrait en créant un répertoire au nom unique et personnalisé comme suit :

L’archive nommée Projet_2017_Duval_Ducamp.zip crée le répertoire Duval_Ducamp qui contient les fichiers que Albert DUVAL et Bernard DUCAMP souhaitent remettre.

Le code remis pour ce livrable correspond à une solution Visual Studio nettoyée : il permet la compilation et le test du projet, mais les fichiers objets et exécutables ne sont pas inclus.

 

Cette archive devra être déposée sur Moodle:

# Critères d’évaluation

Nous serons particulièrement attentifs aux points suivants :

- L’implémentation des fonctionnalités demandées
- La structuration du code
- La clarté du code (nom d'identifiants, commentaires...)
- La justification des choix techniques (format des données, algorithmes)
- Les plus-values et originalités de votre code (modes supplémentaires, fonctionnalités additionnelles...)
- Le strict respect des consignes et des délais