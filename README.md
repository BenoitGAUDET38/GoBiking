# GoBiking

## Description
GoBiking a pour objectif de récupérer les indications afin d'aller d'une adresse de départ à une adresse d'arrivé. L'application a été conçue pour être utilisée dans les villes étant présentes dans l'API de JCDecaux et rendant disponibles les informations des vélos de la ville. L'itinéraire indiqué par l'application est la meilleure entre un itinéraire à pied ou avec un vélo de la ville.

## Etapes pour lancer le projet
### Lancer ActiveMq
*Si ActiveMq n'est pas installé*
- Télécharger activemq
- Mettre le fichier bin du dossier téléchargé dans le path
*Après que ActiveMq soit installé*
- Lancer ActiveMq dans le terminal avec la commande : **activemq start**

## Lancement du projet
### Lancer le proxy cache
- Aller dans ProxyCache/bin/Debug
- Lancer ProxyCache.exe

### Lancer le routing serveur
- Aller dans RoutingServer/bin/Debug
- Lancer RoutingServer.exe

### Lancer le client Java
- Lancer la classe Main du projet HeavyClient en java
