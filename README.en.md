# Welcome to the AstroTargetSelector (ATS) GitHub page
[Version française](README.md)

A tool for calculating the maximum exposure time without field rotation for Alt./Az. astronomical mounts.

***AstroTargetSelector*** (**ATS**) is freeware available for ***Windows*** and ***Android***.

Available in FR and EN (French on a French-language system, English for all other languages).

The **Android version** works **offline** (handy for smart telescope owners).

![AstroTargetSelector](images/ATS_graph_horaire.png)
![AstroTargetSelector](images/ATS_graph_horaire_ModeNuit.png)
<img src="images/Mobile_Liste.png" alt="Target list" width="300">
<img src="images/Mobile_Liste_ModeNuit.png" alt="Target list" width="300">

> [!NOTE]
> The screenshots in this documentation show the French version of the interface.


## Table of contents
- [Principle](#principle)
- [How ATS works](#how-ats-works)
- [Installation](#installation)
    - [PC (Windows)](#pc-windows)
    - [Mobile (Android)](#mobile-android)
- [Usage](#usage)
    - [Windows application](#windows-application)
        - [Menu](#menu)
            - [File menu](#file-menu)
            - [View menu](#view-menu)
            - [Tools menu](#tools-menu)
            - [? menu](#-menu)
        - [Input area](#input-area)
            - [Visualization](#visualization)
            - [Date and time of the observation](#date-and-time-of-the-observation)
            - [Observation settings](#observation-settings)
        - [Filters](#filters)
        - [List of celestial objects](#list-of-celestial-objects)
        - [Celestial object panel](#celestial-object-panel)
            - [Exposure time chart](#exposure-time-chart)
            - [Visualization modes](#visualization-modes)
        - [Settings dialog](#settings-dialog)
        - [Options dialog](#options-dialog)
        - [Mosaic Calculator](#mosaic-calculator)
        - [Catalog updates](#catalog-updates)
        - [Night mode](#night-mode)
        - [Status bar](#status-bar)
        - [About dialog](#about-dialog)
    - [Mobile application](#mobile-application)
        - [Targets tab](#targets-tab)
        - [List filters](#list-filters)
        - [Target details](#target-details)
        - [Settings tab](#settings-tab)
        - [Night mode on mobile](#night-mode-on-mobile)
        - [About tab](#about-tab)
- [Revisions](#revisions)

## Principle

As you probably know, the exposure time with an Alt-Azimuth mount is limited by field rotation.

However, field rotation is not uniform across the sky: it varies with the Alt-Az coordinates.

Equations make it possible to determine the maximum exposure time from these coordinates and from the sensor used to take the picture. Since we are dealing with mounts fitted with sidereal tracking (goto), the focal length no longer matters in the calculation.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

## How ATS works

ATS takes into account your observation site, the date and the sensor of your imaging camera, and from there gives you how the exposure time evolves over the evening (and more). Objects are ranked from the best exposure time to the worst.

No more wasting long minutes looking for the right exposure time for the object you want to photograph, no more wondering which object to photograph tonight: ATS does it for you.

You will even find that it is quite possible to go beyond the famous "30 s maximum exposure time" in many situations. The limit will then be the quality of the sidereal tracking provided by your mount.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

## Installation

***AstroTargetSelector*** comes in two versions:
- A **Windows application**, with all features.
- A lightweight **Android mobile application**: target selection, visibility charts, Moon and Sun, night mode. PC-related features (mount control, planetarium software, Mosaic Calculator) remain in the Windows version.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

### PC (Windows)

To install ***AstroTargetSelector*** on your PC, download and run the file **[AstroTargetSelector_2201.msi](https://github.com/Juani005999/AstroTargetSelector/raw/main/AstroTargetSelector_2201.msi)**.

> [!NOTE]
> A previous installation does not need to be uninstalled: the installer replaces it automatically, and your settings are kept.\
> At startup, ***ATS*** checks whether a new version is available and offers to download it.

#### Publisher confirmation
![ATS installation confirmation](images/ATS_Install_Execute_Confirm.png)

When you run **AstroTargetSelector_2201.msi** on your PC, the ***Microsoft Defender SmartScreen*** window appears. Click ***Run anyway*** to start the installer.

#### ATS setup wizard
To install ATS, follow the steps of the wizard.

![ATS installation step 1](images/ATS_Install_1.png)
![ATS installation step 2](images/ATS_Install_2.png)
![ATS installation step 3](images/ATS_Install_3.png)
![ATS installation step 4](images/ATS_Install_4.png)
![ATS installation step 5](images/ATS_Install_5.png)

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

### Mobile (Android)

Requirements: Android 7.0 or later, on a 64-bit phone.

To install ***AstroTargetSelector*** on your phone:
1. From your phone, download the file **[AstroTargetSelector_Android_2006.apk](https://github.com/Juani005999/AstroTargetSelector/raw/main/AstroTargetSelector_Android_2006.apk)**.
2. Open the downloaded file. As the application is not distributed through the Play Store, Android asks you to allow the installation of unknown apps for your browser (or your file manager): allow it, then go back to the installation.
3. If ***Google Play Protect*** shows a warning, choose ***More details***, then ***Install anyway***.

> [!NOTE]
> To update the application, simply download and install the new version: your settings are kept.\
> The mobile application does not check for new versions by itself: check this page and the [Revisions](#revisions) table.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

## Usage

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

### Windows application
![Main window](images/PC_FenetrePrincipale.png)

The ***ATS*** main window is made up of:
- a [menu](#menu),
- an [input area](#input-area) (visualization mode, date and time, observation settings),
- a [filters](#filters) area,
- the [list of celestial objects](#list-of-celestial-objects) that can be observed,
- the [panel of the selected celestial object](#celestial-object-panel), with its chart,
- a [status bar](#status-bar).

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Menu

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

##### File menu
![File menu](images/PC_Menu_Fichier.png)

The '***File***' menu contains the following items:
- **Update the catalog of celestial objects**\
Downloads the latest version of the celestial objects catalog (see [Catalog updates](#catalog-updates)).
- **Update sensor catalog**\
Downloads the latest version of the sensor catalog.
- **Exit** (***Ctrl+Q***)\
Closes the application.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

##### View menu
![View menu](images/PC_Menu_Affichage.png)

The '***View***' menu contains the following item:
- **Night mode**\
Turns the [night mode](#night-mode) display on or off.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

##### Tools menu
![Tools menu](images/PC_Menu_Outils.png)

The '***Tools***' menu contains the following items:
- **Refresh the list of celestial objects** (***F5***)\
Recalculates the list of celestial objects and their exposure times.
- **Observation settings** (***Ctrl+P***)\
Opens the [Settings](#settings-dialog) dialog.
- **Options** (***Ctrl+O***)\
Opens the [Options](#options-dialog) dialog.
- **AstroSessionOrganizer**\
Starts the ***[AstroSessionOrganizer](https://github.com/Juani005999/AstroSessionOrganizer)*** (***ASO***) application, if it is installed on the computer.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

##### ? menu
![? menu](images/PC_Menu_APropos.png)

The '***?***' menu contains the following item:
- **About** (***Ctrl+H***)\
Opens the [About](#about-dialog) dialog.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Input area
![Input area](images/PC_Zone_Saisie.png)

This area lets you set the visualization mode and the date and time of the observation, and gives access to the observation settings.

> [!TIP]
> The ![AstroSessionOrganizer](images/PC_Icone_ASO.png) icon, at the top right of the window, is a shortcut to the ***[AstroSessionOrganizer](https://github.com/Juani005999/AstroSessionOrganizer)*** application.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

##### Visualization
The visualization mode sets the period over which exposure times are calculated and displayed:
- **Hourly**: the observation evening, starting from the date and time entered.
- **Nights**: the whole night, from 7 pm to 4 am.
- **Monthly**: the coming month.
- **Annual**: the coming year, month by month.

The scoring and ranking of celestial objects depend on the selected visualization mode (see [Visualization modes](#visualization-modes)).

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

##### Date and time of the observation
The date and time entered set the start of the calculation period.\
The ![Refresh](images/PC_Icone_Actualiser.png) button recalculates the list of celestial objects with the date and time entered.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

##### Observation settings
![Settings tooltip](images/PC_InfoBulle_Parametres.png)

This part shows the observation site (latitude and longitude).
- The **Edit** button opens the [Settings](#settings-dialog) dialog.
- When you hover over the ![Information](images/PC_Icone_Info.png) icon, a tooltip summarizes all the observation settings: site, sensor, excluded sky areas, minimum height and maximum shake.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Filters
![Filters](images/PC_Zone_Filtres.png)

The '***Filter results***' area lets you narrow down the list of celestial objects displayed:
- **Search**: searches the name, description and constellation of objects (e.g. *M 31*, *Andromeda*, *Crab*).
- **Type**: type of celestial object (galaxy, planetary nebula, open cluster...).
- **Rank min.**: minimum rank of the objects displayed (from 1 to 5).
- **Magnitude max.**: maximum magnitude of the objects displayed (objects whose magnitude is unknown remain displayed).

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### List of celestial objects
![List of celestial objects](images/PC_Zone_Liste.png)

The list shows the celestial objects that can be observed with the current settings, sorted by descending ***Scoring***: the best targets of the moment are at the top of the list.

For each object, the list shows:
- **Scoring**: average of the maximum exposure times (in seconds) over the calculation period, preceded by the icon of the object's rank:

| Rank | Scoring |
| --- | --- |
| 5 | more than 50 s |
| 4 | from 31 to 50 s |
| 3 | from 21 to 30 s |
| 2 | from 11 to 20 s |
| 1 | 10 s or less |

- **Name**, **Type**, **Description**, **Constellation**.
- **RA**, **DEC**: equatorial coordinates of the object.
- **Azimuth**, **Height**: position of the object at the date and time of the observation.
- **Magnitude**, **Size (W)**, **Size (H)**: magnitude and apparent dimensions of the object.

> [!NOTE]
> An object is considered observable if it is visible (above the minimum height and outside the excluded sky areas) during at least part of the calculation period.

> [!TIP]
> The list can be sorted in ascending or descending order by clicking a column header.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Celestial object panel
When an object is selected in the list, the bottom panel shows its details:
- its rank, name, type and description,
- its coordinates (RA, DEC), position (azimuth, height), magnitude and dimensions,
- the [exposure time chart](#exposure-time-chart) over the calculation period.

The icons to the left of the chart let you:
- ![Help](images/PC_Icone_Aide.png): when you hover over this icon, a tooltip explains what the greyed-out intervals of the chart mean.

![Help tooltip](images/PC_InfoBulle_Rang.png)

- ![Cartes du Ciel](images/PC_Icone_CartesDuCiel.png): show the object in ***Cartes du Ciel***.
- ![Stellarium](images/PC_Icone_Stellarium.png): show the object in ***Stellarium***.
- ![Mosaic Calculator](images/PC_Icone_Mosaic.png): open the [Mosaic Calculator](#mosaic-calculator) for the selected object.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

##### Exposure time chart
![Hourly chart](images/PC_Graphe_Horaire.png)

For each interval of the calculation period, the chart shows:
- as **bars**: the maximum exposure time without field rotation (in seconds), in the color of the interval's rank,
- as a **curve** (***Altitude and direction*** option): the height of the object (in degrees), and its direction under each interval (N, NE, E...),
- the curves and icons of the **Moon** and the **Sun** (***Sun and Moon*** option).

Intervals shown in **grey** mean that the object has entered an excluded area of your sky, or that its height is below the minimum height.

When you hover over an interval, a tooltip shows its details: date and time, maximum exposure time, height and azimuth of the object, sunrise and sunset times, Moon phase and times.

![Interval tooltip](images/PC_InfoBulle_Graphe.png)

In ***Hourly*** mode, the **Duration of an interval** and **Total duration** lists let you adjust how the evening shown in the chart is split.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

##### Visualization modes
The chart adapts to the selected visualization mode:

**Hourly**: the observation evening, interval by interval.

![Hourly chart](images/PC_Graphe_Horaire.png)

**Nights**: the whole night, hour by hour, from 7 pm to 4 am.

![Nights chart](images/PC_Graphe_Nuits.png)

**Monthly**: how things evolve over the coming month. Ideal for planning your next evenings.

![Monthly chart](images/PC_Graphe_Mensuel.png)

**Annual**: how things evolve over the coming year, month by month. Ideal for finding the best season to observe an object.

![Annual chart](images/PC_Graphe_Annuel.png)

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Settings dialog
![Settings dialog](images/PC_Dlg_Parametres.png)

This dialog, opened from the ***Tools > Observation settings*** menu or the **Edit** button, lets you set the observation settings:
- **Place of sighting**: longitude and latitude, in degrees, minutes and seconds.
- **Sensor**: name of your camera's sensor and its width in pixels.\
If your sensor is not in the list, you can enter its name and width in pixels manually.
- **Sky areas excluded**: directions hidden by an obstacle (building, trees, light pollution...). Objects in these directions are considered not observable.
- **Minimum apparent height**: height below which an object is considered not observable (20° by default).
- **Shake max.**: maximum shift tolerated, in pixels, caused by field rotation during an exposure (1 px by default).

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Options dialog
![Options dialog](images/PC_Dlg_Options.png)

This dialog, opened from the ***Tools > Options*** menu, lets you set:
- **Planetarium settings**: the communication options with ***Stellarium*** (server and port of the remote control plugin, by default **localhost** / **8090**) and ***Cartes du Ciel*** (server, by default **127.0.0.1**).
- **Night mode display customization**: window colors and font colors (dark tones and light tones) used in [night mode](#night-mode). The **Reset default colors** button restores the original colors.
- **Language**: language of the interface. In ***Automatic (Windows language)*** mode, the application is displayed in French if Windows is in French, and in English for all other languages. You can also force ***Français*** or ***English***: the change takes effect the next time the application starts.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Mosaic Calculator
![Mosaic Calculator](images/PC_Dlg_MosaicCalculator.png)

The ***Mosaic Calculator*** lets you go beyond your field of view by making a mosaic of the selected object.

Mosaic settings:
- **FOV**: the field of view of your instrument, in degrees. The information icon reminds you of the formula to calculate it.
- **Exposure time per panel**, in minutes.
- **Image overlap**, as a percentage.
- The icons to the right of the **RA** and **Mosaic width** fields let you change the center and size of the mosaic to customize it.

Calculation results:
- the number of panels, the total exposure time and the estimated total width of the mosaic,
- the estimated overall rotation of the object during the session (a warning appears if it is significant),
- the list of panels with their coordinates, and their graphical representation on the right. The selected panel is highlighted.
- for 4-panel mosaics, the option to add an extra central panel.

Available actions:
- show the selected panel or the complete mosaic in ***Stellarium*** or ***Cartes du Ciel***,
- **ASCOM telescope**: connect your ***ASCOM*** telescope and point it at the selected panel (***Slew*** button),
- **Export the results** in ***.txt*** or ***.csv*** format, with the option to include links for ***Unistellar*** telescopes.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Catalog updates
![Catalog update](images/PC_Dlg_MiseAJourCatalogue.png)

The ***File > Update the catalog...*** menus download the latest version of the celestial objects catalog or of the sensor catalog. Click **Next** to start the download.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Night mode
![Night mode](images/PC_FenetrePrincipale_ModeNuit.png)

Night mode, turned on from the ***View > Night mode*** menu, displays the application in dark tones to preserve your night vision during your observations.\
Night mode colors can be customized in the [Options](#options-dialog) dialog.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Status bar
![Status bar](images/PC_BarreDeStatut.png)

The status bar shows the date and time of the observation, the selected celestial object, and the current action during calculations.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### About dialog
![About dialog](images/PC_Dlg_APropos.png)

This dialog shows information about ***AstroTargetSelector***: version, author and description.\
The **AstroTargetSelector GitHub page** link opens this page in your browser: you will find the latest versions (Windows and Android) and what's new.\
The **Open logs file** button opens the application's log file.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

### Mobile application
The mobile application is organized in three tabs: **Targets**, **Settings** and **About**.\
It is available in French and English, depending on the language of your phone.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Targets tab
<img src="images/Mobile_Liste.png" alt="Target list" width="300">

The ***Targets*** tab shows the list of observable targets, sorted from best to worst.

At the top of the screen:
- the **date** and **time** of the observation, which you can change by tapping them,
- the **Now** button, which sets the current date and time,
- the **Filters** button, which shows or hides the [list filters](#list-filters),
- the **Night** / **Day** button, which turns [night mode](#night-mode-on-mobile) on or off,
- the observation site and the number of observable targets, with the calculation period (for example *from 22:00 to 00:00*).

For each target, the list shows:
- its name and rank (from 1 to 5 stars), and a color bar matching the rank,
- its description and type,
- its constellation and magnitude,
- its height, direction and azimuth at the date and time of the observation.

> [!NOTE]
> The mobile list is always calculated over a two-hour evening starting from the time entered (***Hourly*** mode of the Windows version).

> [!TIP]
> Pull the list down to recalculate it.\
> Tap the application icon, at the top left, to open the ***About*** tab.

Tap a target to show its [details](#target-details).

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### List filters
<img src="images/Mobile_Filtres.png" alt="Filters" width="300">

Filters let you narrow down the list of targets displayed:
- **search** in the name, description or constellation,
- target **type**,
- minimum **rank**,
- maximum **magnitude**.

The **Clear** button resets all the filters.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Target details
<img src="images/Mobile_Detail_Horaire.png" alt="Target details" width="300">

Below the name and rank of the target, the details screen shows:
- its description, constellation, magnitude and dimensions,
- its coordinates (RA, DEC),
- its height and azimuth at the date and time of the observation,
- the exposure time chart in the chosen visualization mode: **Hourly**, **Nights**, **Monthly** or **Annual**.

The chart shows the maximum exposure time (bars), the height of the target (curve), and the Moon and the Sun depending on the mode. Tap a point of the chart to show its details below the chart: date and time, maximum exposure time, height and azimuth, Sun and Moon.

In **Hourly** and **Nights** modes, the bottom of the screen shows the Moon phase and the rise and set times of the Moon and the Sun.

<p align="center">
<img src="images/Mobile_Detail_Nuits.png" alt="Nights mode" width="240">
<img src="images/Mobile_Detail_Mensuel.png" alt="Monthly mode" width="240">
<img src="images/Mobile_Detail_Annuel.png" alt="Annual mode" width="240">
</p>

> [!TIP]
> The chosen visualization mode is remembered for the next time.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Settings tab
<img src="images/Mobile_Parametres.png" alt="Settings" width="300">

The ***Settings*** tab lets you set:
- **Night mode**: turns [night mode](#night-mode-on-mobile) on or off.
- **Observation site**: latitude and longitude in decimal degrees. The conversion to degrees, minutes and seconds is shown below each value.\
The **Use the phone's position** button fills in the site automatically from the phone's location.
- **Sensor**: sensor model and width in pixels. If your sensor is not in the list, choose ***Other sensor…*** and enter its name and width.
- **Calculation**: max. shake (in pixels) and minimum height (in degrees).
- **Excluded sky areas**: directions hidden by an obstacle.
- **Observation evening**: interval and duration used by the ***Hourly*** mode chart of the details screen.

Tap **Save** to confirm the settings: the target list is then recalculated.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### Night mode on mobile
<p align="center">
<img src="images/Mobile_Liste_ModeNuit.png" alt="List in night mode" width="300">
<img src="images/Mobile_Detail_ModeNuit.png" alt="Details in night mode" width="300">
</p>

Night mode displays the application in red on black, to preserve your night vision during your observations.\
It is turned on from the **Night** button of the ***Targets*** tab, or from the ***Settings*** tab.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

#### About tab
<img src="images/Mobile_APropos.png" alt="About" width="300">

The ***About*** tab shows the version of the application, its description and its authors.\
The **View on GitHub** button opens this page in the phone's browser: you will find the latest versions and what's new.

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>

<p align="right"><b>Enjoy ;)</b></p>
<p align="right"><b><i>Juanito del Pepito</i></b></p>

## Revisions

| Date | Version | Comments |
| --- | --- | --- |
| 09/10/2026 | 2.2.0.1 | <ul><li>New: Extended catalog: 2,079 celestial objects instead of 244, with the addition of NGC objects (magnitude ≤ 12) and IC objects (magnitude ≤ 12, plus nebulae whose magnitude is unknown).</li><li>New: Unknown magnitude: the magnitude is left empty; these objects remain displayed when filtering by magnitude and are listed last when sorting.</li><li>Fixes: Coordinates of NGC 281, NGC 884 and NGC 4725 corrected.</li><li>Fixes: English translation: all constellations are now translated.</li></ul> |
| 09/10/2026 | 2.1.1.0 | <ul><li>Performance: Information panel: the chart is displayed about 2 times faster when moving from one celestial object to the next (smoother keyboard scrolling).</li></ul> |
| 09/10/2026 | 2.1.0.1 | <ul><li>Performance: Celestial object list: loading and refreshing about 4 times faster, in preparation for a larger catalog.</li></ul> |
| 05/10/2026 | 2.0.0.6 (Android) | <ul><li>Fixes: English translation: celestial object types and constellations are now translated (list, type filter, search).</li><li>Fixes: Monthly chart: the axis dates follow the phone's date format.</li></ul> |
| 05/10/2026 | 2.0.1.8 | <ul><li>Fixes: English translation: celestial object types and constellations are now translated (list, type filter, search).</li></ul> |
| 05/10/2026 | 2.0.1.7 | <ul><li>New: Language: the application is displayed in French on a French-language Windows, and in English for all other languages.</li><li>New: Options: choice of the interface language (Automatic / Français / English).</li><li>Fixes: English translation completed: Mosaic Calculator, Options and ASCOM telescope control.</li></ul> |
| 04/10/2026 | 2.0.0.5 (Android) | <ul><li>New: About: link to the AstroTargetSelector GitHub page (Windows and Android installers, what's new).</li></ul> |
| 04/10/2026 | 2.0.1.6 | <ul><li>New: About: link to the AstroTargetSelector GitHub page (Windows and Android installers, what's new).</li><li>Fixes: Night mode: the download link of the new version window now follows the night mode colors.</li></ul> |
| 04/10/2026 | 2.0.0.4 (Android) | <ul><li>Fixes: Target details: between midnight and 2 am, the rise and set times of the Sun and the Moon were those of the previous day.</li></ul> |
| 04/10/2026 | 2.0.1.5 | <ul><li>Fixes: Details chart: between midnight and 2 am, the rise and set times of the Sun and the Moon were those of the previous day.</li></ul> |
| 04/10/2026 | 2.0.1.4 | <ul><li>Fixes: Moon: on days when the Moon does not rise (or does not set), the Moon information is displayed again, the missing time being replaced by "—".</li></ul> |
| 04/10/2026 | 2.0.1.3 | <ul><li>Maintenance: Update checks and configuration file downloads (celestial objects, sensors) are now done from GitHub.</li></ul> |
| 03/10/2026 | 2.0.0.3 (Android) | <ul><li>The application no longer requests Internet access: it works entirely offline, only location is requested (optional).</li></ul> |
| 03/10/2026 | 2.0.0.2 (Android) | <ul><li>Target details: the chart is recalculated when the settings are changed (sensor, max. shake, interval, duration…).</li><li>Settings: save area highlighted.</li></ul> |
| 03/10/2026 | 2.0.1.2 | <ul><li>New application icon, shared with the mobile version.</li><li>AstroTargetSelector is now available on Android (see Installation on Android).</li><li>Default minimum height set to 20° (new installations).</li><li>English translation: Moon phase names and Moon / Sun labels.</li></ul> |
| 03/10/2026 | 2.0.0.1 (Android) | <ul><li>First mobile version for Android: list of observable targets with filters, target details with Hourly, Nights, Monthly and Annual charts, Moon and Sun.</li><li>Settings: observation site (entered or from the phone's GPS position), sensor (list or custom sensor), max. shake, minimum height, excluded sky areas.</li><li>Red on black night mode, in French and English.</li></ul> |
| 27/09/2026 | 2.0.1.1 | <ul><li>Maintenance: Technical libraries update.</li><li>Maintenance: Improved logging of update checks.</li></ul> |
| 27/09/2026 | 2.0.1.0 | <ul><li>New architecture: Technical overhaul of the application, in preparation for a future mobile version (Android).</li><li>Settings: Your settings (observation site, sensor, colors, options…) are now kept when updating.</li><li>Update check: the availability of a new version is correctly detected again.</li><li>Taskbar: the AstroTargetSelector icon is displayed correctly when the application starts in full screen.</li><li>English translation completed.</li></ul> |
| 16/07/2026 | 1.8.0.1 | <ul><li>FTP credentials update.</li></ul> |
| 16/07/2023 | 1.7.0.2 | <ul><li>Mosaic Calculator: ASCOM telescope control and sending of panel coordinates to the telescope.</li><li>Mosaic Calculator: Option to show the complete mosaic in Stellarium and Cartes du Ciel.</li></ul> |
| 09/03/2023 | 1.6.4.1 | <ul><li>Mosaic Calculator: Option to add an extra central panel for 4-panel mosaics.</li></ul> |
| 24/02/2023 | 1.6.3.2 | <ul><li>Mosaic Calculator: Option to change the center and size of the mosaic to customize it.</li></ul> |
| 20/02/2023 | 1.6.1.2 | <ul><li>Mosaic Calculator: Fixed the RA value in Unistellar links.</li></ul> |
| 19/02/2023 | 1.6.1.1 | <ul><li>New feature: the Mosaic Calculator.<br/>Go beyond your field of view with the mosaic calculator.</li></ul> |
| 18/02/2023 | 1.5.4.2 | <ul><li>The Sun meets the Moon: the Sun is displayed in Hourly and Night modes, and in the tooltip in Monthly mode.</li></ul> |
| 04/02/2023 | 1.5.3.1 | <ul><li>Moon icon: Fixed the FirstQuarter and LastQuarter icons.</li><li>Tooltips: Added the Moon azimuth.</li><li>Options: New Options dialog.</li></ul> |
| 02/02/2023 | 1.5.2.1 | <ul><li>Welcome to the Moon: the Moon is displayed in Hourly, Night and Monthly modes.</li></ul> |
| 29/01/2023 | 1.4.0.2 | <ul><li>Night mode custom colors: night mode display colors can be customized.</li></ul> |
| 24/01/2023 | 1.4.0.1 | <ul><li>TCP Server: the object's designations are added when the command is sent.</li><li>Show in Stellarium: Stellarium is brought to the foreground when an object is displayed.</li><li>Show in Cartes du Ciel: Cartes du Ciel is brought to the foreground when an object is displayed.</li></ul> |
| 22/01/2023 | 1.3.0.1 | <ul><li>TCP Server: AstroTargetSelector can now receive commands from other software.</li></ul> |
| 30/08/2022 | 1.1.0.1 | <ul><li>Initial version.</li></ul> |

<p align="right"><a href="#table-of-contents">Back to table of contents</a></p>
