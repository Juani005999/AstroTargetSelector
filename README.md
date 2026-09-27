# Bienvenue sur la page Github de AstroTargetSelector (ATS)
Utilitaire permettant le calcul du temps de pose maximum sans rotation de champ pour les montures astronomiques de type Alt./Az.

![AstroTargetSelector](images/ATS_graph_horaire.png)
![AstroTargetSelector](images/ATS_graph_horaire_ModeNuit.png)

## Principe 

Vous le savez certainement, le temps de pose avec une monture Alt-Azimutale est limité par la rotation de champ.

Or, la rotation de champ n'est pas homogène sur la voute céleste, elle varie en fonction des coordonnées Alt-Az.

Des équations permettent de déterminer le temps de pose maximum en fonction de ces coordonnées et du capteur utilisé pour réaliser la photo. Etant donné que l'on va s'intéresser à des montures équipées d'un système de suivi sidéral (goto), la focale n'entre plus en compte dans le calcul.

## Fonctionnement d'ATS

ATS va considérer votre lieu d'observation, la date, le capteur de l'imageur et à partir de cela, va vous donner l'évolution du temps de pose sur la soirée (mais pas que). Les objets sont notés du meilleur temps au plus mauvais.

Plus besoin de perdre de longues minutes à chercher le bon temps de pose pour l'objet que vous souhaitez photographier, plus besoin de vous demander quel objet photographier ce soir, ATS se charge de cela pour vous.

Vous allez même vous apercevoir qu'il est tout à fait possible de dépasser les fameuses "30s de temps de pose max" dans de nombreuses situations. La limite sera alors celle de la qualité du suivi sidéral offert par votre monture.

## Installation d'ATS

Pour installer ***AstroTargetSelector*** sur votre PC, téléchargez et lancez le fichier **AstroTargetSelector_2010.msi**.

### Confirmation de l'éditeur
![ATS Installation confirmation](images/ATS_Install_Execute_Confirm.png)\
Lors de l'exécution de **AstroTargetSelector_2010.msi** sur votre PC, l'écran de ***Microsoft Defender SmartScreen*** apparait. Vous pouvez cliquer sur ***Exécuter quand même*** afin de lancer l'installeur.

### Assistant d'installation d'ATS
Afin d'installer le logiciel ATS, veuillez suivre les étapes de l'assistant.
![ATS Installation step 1](images/ATS_Install_1.png)
![ATS Installation step 2](images/ATS_Install_2.png)
![ATS Installation step 3](images/ATS_Install_3.png)
![ATS Installation step 4](images/ATS_Install_4.png)
![ATS Installation step 5](images/ATS_Install_5.png)

\
<p align="right"><b>Enjoy ;)</b></p>
<p align="right"><b><i>Juanito del Pepito</i></b></p>


## Révisions

| Date | Version | Commentaires |
| --- | --- | --- |
| 27/09/2026 | 2.0.1.0 | <ul><li>Nouvelle architecture : Refonte technique de l’application, en préparation d’une future version mobile (Android).</li><li>Paramètres : Vos paramètres (lieu d’observation, capteur, couleurs, options…) sont désormais conservés lors des mises à jour.</li><li>Vérification des mises à jour : La disponibilité d’une nouvelle version est de nouveau correctement détectée.</li><li>Barre des tâches : L’icône d’AstroTargetSelector s’affiche correctement lorsque l’application démarre en plein écran.</li><li>Traduction anglaise complétée.</li></ul> |
| 16/07/2026 | 1.8.0.1 | <ul><li>Mise à jour des Credentials FTP.</li></ul> |
| 16/07/2023 | 1.7.0.2 | <ul><li>Mosaic Calculator : Pilotage du télescope ASCOM et envoi des coordonnées des panneaux au télescope.</li><li>Mosaic Calculator : Possibilitté d’afficher la mosaïque complète dans Stellarium et Cartes du Ciel.</li></ul> |
| 09/03/2023 | 1.6.4.1 | <ul><li>Mosaic Calculator : Possibilité d’ajouter un panneau central supplémentaire pour les mosaïques à 4 panneaux.</li></ul> |
| 24/02/2023 | 1.6.3.2 | <ul><li>Mosaic Calculator : Possibilité de modifier le centre et la taille de la mosaïque afin de pouvoir la personnaliser.</li></ul> |
| 20/02/2023 | 1.6.1.2 | <ul><li>Mosaic Calculator : Correction de la valeur du ra dans les liens Unistellar.</li></ul> |
| 19/02/2023 | 1.6.1.1 | <ul><li>Nouvelle fonctionnalité : Le Mosaic Calculator.<br/>Dépassez votre champ de vision grâce au calculateur de mosaïque.</li></ul> |
| 18/02/2023 | 1.5.4.2 | <ul><li>Le Soleil à rendez-vous avec la Lune : Affichage du Soleil pour les modes Horaire et Nuit, et dans l’info-bulle pour le mode Mensuel.</li></ul> |
| 04/02/2023 | 1.5.3.1 | <ul><li>Icone la Lune : Correction des icones FirstQuarter et LastQuarter.</li><li>Infos bulle : Ajout de l’information Azimut de la Lune.</li><li>Options : Nouvelle boîte de dialogue Options.</li></ul> |
| 02/02/2023 | 1.5.2.1 | <ul><li>Bienvenue à la Lune : Affichage de la lune pour les modes Horaire, Nuit et Mensuel.</li></ul> |
| 29/01/2023 | 1.4.0.2 | <ul><li>Mode nuit custom colors : Personnalisation des couleurs d’affichage du mode nuit.</li></ul> |
| 24/01/2023 | 1.4.0.1 | <ul><li>TCP Server : Ajout des dénominations de l’objet lors de l’envoi de la commande.</li><li>Ouverture dans Stellarium : Mise en avant plan de Stellarium lors de l’affichage d’un objet.</li><li>Ouverture dans Carte du Ciel : Mise en avant plan de Carte du Ciel lors de l’affichage d’un objet.</li></ul> |
| 22/01/2023 | 1.3.0.1 | <ul><li>TCP Server : AstroTargetSelector a désormais la possibilité de recevoir des commandes depuis d’autre logiciels.</li></ul> |
| 30/08/2022 | 1.1.0.1 | <ul><li>Version initiale.</li></ul> |
