const cities =  [
  {
      "Name": "Airdrie",
      "Province": "Alberta"
  },
  {
      "Name": "Brooks",
      "Province": "Alberta"
  },
  {
      "Name": "Calgary",
      "Province": "Alberta"
  },
  {
      "Name": "Camrose",
      "Province": "Alberta"
  },
  {
      "Name": "Chestermere",
      "Province": "Alberta"
  },
  {
      "Name": "Cold Lake",
      "Province": "Alberta"
  },
  {
      "Name": "Edmonton",
      "Province": "Alberta"
  },
  {
      "Name": "Fort Saskatchewan",
      "Province": "Alberta"
  },
  {
      "Name": "Grande Prairie",
      "Province": "Alberta"
  },
  {
      "Name": "Lacombe",
      "Province": "Alberta"
  },
  {
      "Name": "Leduc",
      "Province": "Alberta"
  },
  {
      "Name": "Lethbridge",
      "Province": "Alberta"
  },
  {
      "Name": "Lloydminster (part)",
      "Province": "Alberta"
  },
  {
      "Name": "Medicine Hat",
      "Province": "Alberta"
  },
  {
      "Name": "Red Deer",
      "Province": "Alberta"
  },
  {
      "Name": "Spruce Grove",
      "Province": "Alberta"
  },
  {
      "Name": "St. Albert",
      "Province": "Alberta"
  },
  {
      "Name": "Wetaskiwin",
      "Province": "Alberta"
  },
  {
      "Name": "Abbotsford",
      "Province": "British Columbia"
  },
  {
      "Name": "Armstrong",
      "Province": "British Columbia"
  },
  {
      "Name": "Burnaby",
      "Province": "British Columbia"
  },
  {
      "Name": "Campbell River",
      "Province": "British Columbia"
  },
  {
      "Name": "Castlegar",
      "Province": "British Columbia"
  },
  {
      "Name": "Chilliwack",
      "Province": "British Columbia"
  },
  {
      "Name": "Colwood",
      "Province": "British Columbia"
  },
  {
      "Name": "Coquitlam",
      "Province": "British Columbia"
  },
  {
      "Name": "Courtenay",
      "Province": "British Columbia"
  },
  {
      "Name": "Cranbrook",
      "Province": "British Columbia"
  },
  {
      "Name": "Dawson Creek",
      "Province": "British Columbia"
  },
  {
      "Name": "Duncan",
      "Province": "British Columbia"
  },
  {
      "Name": "Enderby",
      "Province": "British Columbia"
  },
  {
      "Name": "Fernie",
      "Province": "British Columbia"
  },
  {
      "Name": "Fort St. John",
      "Province": "British Columbia"
  },
  {
      "Name": "Grand Forks",
      "Province": "British Columbia"
  },
  {
      "Name": "Greenwood",
      "Province": "British Columbia"
  },
  {
      "Name": "Kamloops",
      "Province": "British Columbia"
  },
  {
      "Name": "Kelowna",
      "Province": "British Columbia"
  },
  {
      "Name": "Kimberley",
      "Province": "British Columbia"
  },
  {
      "Name": "Langford",
      "Province": "British Columbia"
  },
  {
      "Name": "Langley",
      "Province": "British Columbia"
  },
  {
      "Name": "Maple Ridge",
      "Province": "British Columbia"
  },
  {
      "Name": "Merritt",
      "Province": "British Columbia"
  },
  {
      "Name": "Nanaimo",
      "Province": "British Columbia"
  },
  {
      "Name": "Nelson",
      "Province": "British Columbia"
  },
  {
      "Name": "New Westminster",
      "Province": "British Columbia"
  },
  {
      "Name": "North Vancouver",
      "Province": "British Columbia"
  },
  {
      "Name": "Parksville",
      "Province": "British Columbia"
  },
  {
      "Name": "Penticton",
      "Province": "British Columbia"
  },
  {
      "Name": "Pitt Meadows",
      "Province": "British Columbia"
  },
  {
      "Name": "Port Alberni",
      "Province": "British Columbia"
  },
  {
      "Name": "Port Coquitlam",
      "Province": "British Columbia"
  },
  {
      "Name": "Port Moody",
      "Province": "British Columbia"
  },
  {
      "Name": "Powell River",
      "Province": "British Columbia"
  },
  {
      "Name": "Prince George",
      "Province": "British Columbia"
  },
  {
      "Name": "Prince Rupert",
      "Province": "British Columbia"
  },
  {
      "Name": "Quesnel",
      "Province": "British Columbia"
  },
  {
      "Name": "Revelstoke",
      "Province": "British Columbia"
  },
  {
      "Name": "Richmond",
      "Province": "British Columbia"
  },
  {
      "Name": "Rossland",
      "Province": "British Columbia"
  },
  {
      "Name": "Salmon Arm",
      "Province": "British Columbia"
  },
  {
      "Name": "Surrey",
      "Province": "British Columbia"
  },
  {
      "Name": "Terrace",
      "Province": "British Columbia"
  },
  {
      "Name": "Trail",
      "Province": "British Columbia"
  },
  {
      "Name": "Vancouver",
      "Province": "British Columbia"
  },
  {
      "Name": "Vernon",
      "Province": "British Columbia"
  },
  {
      "Name": "Victoria",
      "Province": "British Columbia"
  },
  {
      "Name": "White Rock",
      "Province": "British Columbia"
  },
  {
      "Name": "Williams Lake",
      "Province": "British Columbia"
  },
  {
      "Name": "Brandon",
      "Province": "Manitoba"
  },
  {
      "Name": "Dauphin",
      "Province": "Manitoba"
  },
  {
      "Name": "Flin Flon (part)",
      "Province": "Manitoba"
  },
  {
      "Name": "Morden",
      "Province": "Manitoba"
  },
  {
      "Name": "Portage la Prairie",
      "Province": "Manitoba"
  },
  {
      "Name": "Selkirk",
      "Province": "Manitoba"
  },
  {
      "Name": "Steinbach",
      "Province": "Manitoba"
  },
  {
      "Name": "Thompson",
      "Province": "Manitoba"
  },
  {
      "Name": "Winkler",
      "Province": "Manitoba"
  },
  {
      "Name": "Winnipeg",
      "Province": "Manitoba"
  },
  {
      "Name": "Bathurst",
      "Province": "New Brunswick"
  },
  {
      "Name": "Campbellton",
      "Province": "New Brunswick"
  },
  {
      "Name": "Dieppe",
      "Province": "New Brunswick"
  },
  {
      "Name": "Edmundston",
      "Province": "New Brunswick"
  },
  {
      "Name": "Fredericton",
      "Province": "New Brunswick"
  },
  {
      "Name": "Miramichi",
      "Province": "New Brunswick"
  },
  {
      "Name": "Moncton",
      "Province": "New Brunswick"
  },
  {
      "Name": "Saint John",
      "Province": "New Brunswick"
  },
  {
      "Name": "Corner Brook",
      "Province": "Newfoundland and Labrador"
  },
  {
      "Name": "Mount Pearl",
      "Province": "Newfoundland and Labrador"
  },
  {
      "Name": "St. John's",
      "Province": "Newfoundland and Labrador"
  },
  {
      "Name": "Yellowknife",
      "Province": "Northwest Territories"
  },
  {
      "Name": "Iqaluit",
      "Province": "Nunavut"
  },
  {
      "Name": "Barrie",
      "Province": "Ontario"
  },
  {
      "Name": "Belleville",
      "Province": "Ontario"
  },
  {
      "Name": "Brampton",
      "Province": "Ontario"
  },
  {
      "Name": "Brant",
      "Province": "Ontario"
  },
  {
      "Name": "Brantford",
      "Province": "Ontario"
  },
  {
      "Name": "Brockville",
      "Province": "Ontario"
  },
  {
      "Name": "Burlington",
      "Province": "Ontario"
  },
  {
      "Name": "Cambridge",
      "Province": "Ontario"
  },
  {
      "Name": "Clarence-Rockland",
      "Province": "Ontario"
  },
  {
      "Name": "Cornwall",
      "Province": "Ontario"
  },
  {
      "Name": "Dryden",
      "Province": "Ontario"
  },
  {
      "Name": "Elliot Lake",
      "Province": "Ontario"
  },
  {
      "Name": "Greater Sudbury",
      "Province": "Ontario"
  },
  {
      "Name": "Guelph",
      "Province": "Ontario"
  },
  {
      "Name": "Haldimand County",
      "Province": "Ontario"
  },
  {
      "Name": "Hamilton",
      "Province": "Ontario"
  },
  {
      "Name": "Kawartha Lakes",
      "Province": "Ontario"
  },
  {
      "Name": "Kenora",
      "Province": "Ontario"
  },
  {
      "Name": "Kingston",
      "Province": "Ontario"
  },
  {
      "Name": "Kitchener",
      "Province": "Ontario"
  },
  {
      "Name": "London",
      "Province": "Ontario"
  },
  {
      "Name": "Markham",
      "Province": "Ontario"
  },
  {
      "Name": "Mississauga",
      "Province": "Ontario"
  },
  {
      "Name": "Niagara Falls",
      "Province": "Ontario"
  },
  {
      "Name": "Norfolk County",
      "Province": "Ontario"
  },
  {
      "Name": "North Bay",
      "Province": "Ontario"
  },
  {
      "Name": "Orillia",
      "Province": "Ontario"
  },
  {
      "Name": "Oshawa",
      "Province": "Ontario"
  },
  {
      "Name": "Ottawa",
      "Province": "Ontario"
  },
  {
      "Name": "Owen Sound",
      "Province": "Ontario"
  },
  {
      "Name": "Pembroke",
      "Province": "Ontario"
  },
  {
      "Name": "Peterborough",
      "Province": "Ontario"
  },
  {
      "Name": "Pickering",
      "Province": "Ontario"
  },
  {
      "Name": "Port Colborne",
      "Province": "Ontario"
  },
  {
      "Name": "Prince Edward County",
      "Province": "Ontario"
  },
  {
      "Name": "Quinte West",
      "Province": "Ontario"
  },
  {
      "Name": "Sarnia",
      "Province": "Ontario"
  },
  {
      "Name": "Sault Ste. Marie",
      "Province": "Ontario"
  },
  {
      "Name": "St. Catharines",
      "Province": "Ontario"
  },
  {
      "Name": "St. Thomas",
      "Province": "Ontario"
  },
  {
      "Name": "Stratford",
      "Province": "Ontario"
  },
  {
      "Name": "Temiskaming Shores",
      "Province": "Ontario"
  },
  {
      "Name": "Thorold",
      "Province": "Ontario"
  },
  {
      "Name": "Thunder Bay",
      "Province": "Ontario"
  },
  {
      "Name": "Timmins",
      "Province": "Ontario"
  },
  {
      "Name": "Toronto",
      "Province": "Ontario"
  },
  {
      "Name": "Vaughan",
      "Province": "Ontario"
  },
  {
      "Name": "Waterloo",
      "Province": "Ontario"
  },
  {
      "Name": "Welland",
      "Province": "Ontario"
  },
  {
      "Name": "Windsor",
      "Province": "Ontario"
  },
  {
      "Name": "Woodstock",
      "Province": "Ontario"
  },
  {
      "Name": "Charlottetown",
      "Province": "Prince Edward Island"
  },
  {
      "Name": "Summerside",
      "Province": "Prince Edward Island"
  },
  {
      "Name": "Acton Vale",
      "Province": "Quebec"
  },
  {
      "Name": "Alma",
      "Province": "Quebec"
  },
  {
      "Name": "Amos",
      "Province": "Quebec"
  },
  {
      "Name": "Amqui",
      "Province": "Quebec"
  },
  {
      "Name": "Asbestos",
      "Province": "Quebec"
  },
  {
      "Name": "Baie-Comeau",
      "Province": "Quebec"
  },
  {
      "Name": "Baie-D'Urfé",
      "Province": "Quebec"
  },
  {
      "Name": "Baie-Saint-Paul",
      "Province": "Quebec"
  },
  {
      "Name": "Barkmere",
      "Province": "Quebec"
  },
  {
      "Name": "Beaconsfield",
      "Province": "Quebec"
  },
  {
      "Name": "Beauceville",
      "Province": "Quebec"
  },
  {
      "Name": "Beauharnois",
      "Province": "Quebec"
  },
  {
      "Name": "Beaupré",
      "Province": "Quebec"
  },
  {
      "Name": "Bécancour",
      "Province": "Quebec"
  },
  {
      "Name": "Bedford",
      "Province": "Quebec"
  },
  {
      "Name": "Belleterre",
      "Province": "Quebec"
  },
  {
      "Name": "Beloeil",
      "Province": "Quebec"
  },
  {
      "Name": "Berthierville",
      "Province": "Quebec"
  },
  {
      "Name": "Blainville",
      "Province": "Quebec"
  },
  {
      "Name": "Boisbriand",
      "Province": "Quebec"
  },
  {
      "Name": "Bois-des-Filion",
      "Province": "Quebec"
  },
  {
      "Name": "Bonaventure",
      "Province": "Quebec"
  },
  {
      "Name": "Boucherville",
      "Province": "Quebec"
  },
  {
      "Name": "Brome Lake",
      "Province": "Quebec"
  },
  {
      "Name": "Bromont",
      "Province": "Quebec"
  },
  {
      "Name": "Brossard",
      "Province": "Quebec"
  },
  {
      "Name": "Brownsburg-Chatham",
      "Province": "Quebec"
  },
  {
      "Name": "Candiac",
      "Province": "Quebec"
  },
  {
      "Name": "Cap-Chat",
      "Province": "Quebec"
  },
  {
      "Name": "Cap-Santé",
      "Province": "Quebec"
  },
  {
      "Name": "Carignan",
      "Province": "Quebec"
  },
  {
      "Name": "Carleton-sur-Mer",
      "Province": "Quebec"
  },
  {
      "Name": "Causapscal",
      "Province": "Quebec"
  },
  {
      "Name": "Chambly",
      "Province": "Quebec"
  },
  {
      "Name": "Chandler",
      "Province": "Quebec"
  },
  {
      "Name": "Chapais",
      "Province": "Quebec"
  },
  {
      "Name": "Charlemagne",
      "Province": "Quebec"
  },
  {
      "Name": "Châteauguay",
      "Province": "Quebec"
  },
  {
      "Name": "Château-Richer",
      "Province": "Quebec"
  },
  {
      "Name": "Chibougamau",
      "Province": "Quebec"
  },
  {
      "Name": "Clermont",
      "Province": "Quebec"
  },
  {
      "Name": "Coaticook",
      "Province": "Quebec"
  },
  {
      "Name": "Contrecoeur",
      "Province": "Quebec"
  },
  {
      "Name": "Cookshire-Eaton",
      "Province": "Quebec"
  },
  {
      "Name": "Côte Saint-Luc",
      "Province": "Quebec"
  },
  {
      "Name": "Coteau-du-Lac",
      "Province": "Quebec"
  },
  {
      "Name": "Cowansville",
      "Province": "Quebec"
  },
  {
      "Name": "Danville",
      "Province": "Quebec"
  },
  {
      "Name": "Daveluyville",
      "Province": "Quebec"
  },
  {
      "Name": "Dégelis",
      "Province": "Quebec"
  },
  {
      "Name": "Delson",
      "Province": "Quebec"
  },
  {
      "Name": "Desbiens",
      "Province": "Quebec"
  },
  {
      "Name": "Deux-Montagnes",
      "Province": "Quebec"
  },
  {
      "Name": "Disraeli",
      "Province": "Quebec"
  },
  {
      "Name": "Dolbeau-Mistassini",
      "Province": "Quebec"
  },
  {
      "Name": "Dollard-des-Ormeaux",
      "Province": "Quebec"
  },
  {
      "Name": "Donnacona",
      "Province": "Quebec"
  },
  {
      "Name": "Dorval",
      "Province": "Quebec"
  },
  {
      "Name": "Drummondville",
      "Province": "Quebec"
  },
  {
      "Name": "Dunham",
      "Province": "Quebec"
  },
  {
      "Name": "Duparquet",
      "Province": "Quebec"
  },
  {
      "Name": "East Angus",
      "Province": "Quebec"
  },
  {
      "Name": "Estérel",
      "Province": "Quebec"
  },
  {
      "Name": "Farnham",
      "Province": "Quebec"
  },
  {
      "Name": "Fermont",
      "Province": "Quebec"
  },
  {
      "Name": "Forestville",
      "Province": "Quebec"
  },
  {
      "Name": "Fossambault-sur-le-Lac",
      "Province": "Quebec"
  },
  {
      "Name": "Gaspé",
      "Province": "Quebec"
  },
  {
      "Name": "Gatineau",
      "Province": "Quebec"
  },
  {
      "Name": "Gracefield",
      "Province": "Quebec"
  },
  {
      "Name": "Granby",
      "Province": "Quebec"
  },
  {
      "Name": "Grande-Rivière",
      "Province": "Quebec"
  },
  {
      "Name": "Hampstead",
      "Province": "Quebec"
  },
  {
      "Name": "Hudson",
      "Province": "Quebec"
  },
  {
      "Name": "Huntingdon",
      "Province": "Quebec"
  },
  {
      "Name": "Joliette",
      "Province": "Quebec"
  },
  {
      "Name": "Kingsey Falls",
      "Province": "Quebec"
  },
  {
      "Name": "Kirkland",
      "Province": "Quebec"
  },
  {
      "Name": "La Malbaie",
      "Province": "Quebec"
  },
  {
      "Name": "La Pocatière",
      "Province": "Quebec"
  },
  {
      "Name": "La Prairie",
      "Province": "Quebec"
  },
  {
      "Name": "La Sarre",
      "Province": "Quebec"
  },
  {
      "Name": "La Tuque",
      "Province": "Quebec"
  },
  {
      "Name": "Lac-Delage",
      "Province": "Quebec"
  },
  {
      "Name": "Lachute",
      "Province": "Quebec"
  },
  {
      "Name": "Lac-Mégantic",
      "Province": "Quebec"
  },
  {
      "Name": "Lac-Saint-Joseph",
      "Province": "Quebec"
  },
  {
      "Name": "Lac-Sergent",
      "Province": "Quebec"
  },
  {
      "Name": "L'Ancienne-Lorette",
      "Province": "Quebec"
  },
  {
      "Name": "L'Assomption",
      "Province": "Quebec"
  },
  {
      "Name": "Laval",
      "Province": "Quebec"
  },
  {
      "Name": "Lavaltrie",
      "Province": "Quebec"
  },
  {
      "Name": "Lebel-sur-Quévillon",
      "Province": "Quebec"
  },
  {
      "Name": "L'Épiphanie",
      "Province": "Quebec"
  },
  {
      "Name": "Léry",
      "Province": "Quebec"
  },
  {
      "Name": "Lévis",
      "Province": "Quebec"
  },
  {
      "Name": "L'Île-Cadieux",
      "Province": "Quebec"
  },
  {
      "Name": "L'Île-Dorval",
      "Province": "Quebec"
  },
  {
      "Name": "L'Île-Perrot",
      "Province": "Quebec"
  },
  {
      "Name": "Longueuil",
      "Province": "Quebec"
  },
  {
      "Name": "Lorraine",
      "Province": "Quebec"
  },
  {
      "Name": "Louiseville",
      "Province": "Quebec"
  },
  {
      "Name": "Macamic",
      "Province": "Quebec"
  },
  {
      "Name": "Magog",
      "Province": "Quebec"
  },
  {
      "Name": "Malartic",
      "Province": "Quebec"
  },
  {
      "Name": "Maniwaki",
      "Province": "Quebec"
  },
  {
      "Name": "Marieville",
      "Province": "Quebec"
  },
  {
      "Name": "Mascouche",
      "Province": "Quebec"
  },
  {
      "Name": "Matagami",
      "Province": "Quebec"
  },
  {
      "Name": "Matane",
      "Province": "Quebec"
  },
  {
      "Name": "Mercier",
      "Province": "Quebec"
  },
  {
      "Name": "Métabetchouan–Lac-à-la-Croix",
      "Province": "Quebec"
  },
  {
      "Name": "Métis-sur-Mer",
      "Province": "Quebec"
  },
  {
      "Name": "Mirabel",
      "Province": "Quebec"
  },
  {
      "Name": "Mont-Joli",
      "Province": "Quebec"
  },
  {
      "Name": "Mont-Laurier",
      "Province": "Quebec"
  },
  {
      "Name": "Montmagny",
      "Province": "Quebec"
  },
  {
      "Name": "Montreal",
      "Province": "Quebec"
  },
  {
      "Name": "Montreal West",
      "Province": "Quebec"
  },
  {
      "Name": "Montréal-Est",
      "Province": "Quebec"
  },
  {
      "Name": "Mont-Saint-Hilaire",
      "Province": "Quebec"
  },
  {
      "Name": "Mont-Tremblant",
      "Province": "Quebec"
  },
  {
      "Name": "Mount Royal",
      "Province": "Quebec"
  },
  {
      "Name": "Murdochville",
      "Province": "Quebec"
  },
  {
      "Name": "Neuville",
      "Province": "Quebec"
  },
  {
      "Name": "New Richmond",
      "Province": "Quebec"
  },
  {
      "Name": "Nicolet",
      "Province": "Quebec"
  },
  {
      "Name": "Normandin",
      "Province": "Quebec"
  },
  {
      "Name": "Notre-Dame-de-l'Île-Perrot",
      "Province": "Quebec"
  },
  {
      "Name": "Notre-Dame-des-Prairies",
      "Province": "Quebec"
  },
  {
      "Name": "Otterburn Park",
      "Province": "Quebec"
  },
  {
      "Name": "Paspébiac",
      "Province": "Quebec"
  },
  {
      "Name": "Percé",
      "Province": "Quebec"
  },
  {
      "Name": "Pincourt",
      "Province": "Quebec"
  },
  {
      "Name": "Plessisville",
      "Province": "Quebec"
  },
  {
      "Name": "Pohénégamook",
      "Province": "Quebec"
  },
  {
      "Name": "Pointe-Claire",
      "Province": "Quebec"
  },
  {
      "Name": "Pont-Rouge",
      "Province": "Quebec"
  },
  {
      "Name": "Port-Cartier",
      "Province": "Quebec"
  },
  {
      "Name": "Portneuf",
      "Province": "Quebec"
  },
  {
      "Name": "Prévost",
      "Province": "Quebec"
  },
  {
      "Name": "Princeville",
      "Province": "Quebec"
  },
  {
      "Name": "Québec",
      "Province": "Quebec"
  },
  {
      "Name": "Repentigny",
      "Province": "Quebec"
  },
  {
      "Name": "Richelieu",
      "Province": "Quebec"
  },
  {
      "Name": "Richmond",
      "Province": "Quebec"
  },
  {
      "Name": "Rimouski",
      "Province": "Quebec"
  },
  {
      "Name": "Rivière-du-Loup",
      "Province": "Quebec"
  },
  {
      "Name": "Rivière-Rouge",
      "Province": "Quebec"
  },
  {
      "Name": "Roberval",
      "Province": "Quebec"
  },
  {
      "Name": "Rosemère",
      "Province": "Quebec"
  },
  {
      "Name": "Rouyn-Noranda",
      "Province": "Quebec"
  },
  {
      "Name": "Saguenay",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Augustin-de-Desmaures",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Basile",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Basile-le-Grand",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Bruno-de-Montarville",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Césaire",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Colomban",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Constant",
      "Province": "Quebec"
  },
  {
      "Name": "Sainte-Adèle",
      "Province": "Quebec"
  },
  {
      "Name": "Sainte-Agathe-des-Monts",
      "Province": "Quebec"
  },
  {
      "Name": "Sainte-Anne-de-Beaupré",
      "Province": "Quebec"
  },
  {
      "Name": "Sainte-Anne-de-Bellevue",
      "Province": "Quebec"
  },
  {
      "Name": "Sainte-Anne-des-Monts",
      "Province": "Quebec"
  },
  {
      "Name": "Sainte-Anne-des-Plaines",
      "Province": "Quebec"
  },
  {
      "Name": "Sainte-Catherine",
      "Province": "Quebec"
  },
  {
      "Name": "Sainte-Catherine-de-la-Jacques-Cartier",
      "Province": "Quebec"
  },
  {
      "Name": "Sainte-Julie",
      "Province": "Quebec"
  },
  {
      "Name": "Sainte-Marguerite-du-Lac-Masson",
      "Province": "Quebec"
  },
  {
      "Name": "Sainte-Marie",
      "Province": "Quebec"
  },
  {
      "Name": "Sainte-Marthe-sur-le-Lac",
      "Province": "Quebec"
  },
  {
      "Name": "Sainte-Thérèse",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Eustache",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Félicien",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Gabriel",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Georges",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Hyacinthe",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Jean-sur-Richelieu",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Jérôme",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Joseph-de-Beauce",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Joseph-de-Sorel",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Lambert",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Lazare",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Lin-Laurentides",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Marc-des-Carrières",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Ours",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Pamphile",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Pascal",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Pie",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Raymond",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Rémi",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Sauveur",
      "Province": "Quebec"
  },
  {
      "Name": "Saint-Tite",
      "Province": "Quebec"
  },
  {
      "Name": "Salaberry-de-Valleyfield",
      "Province": "Quebec"
  },
  {
      "Name": "Schefferville",
      "Province": "Quebec"
  },
  {
      "Name": "Scotstown",
      "Province": "Quebec"
  },
  {
      "Name": "Senneterre",
      "Province": "Quebec"
  },
  {
      "Name": "Sept-Îles",
      "Province": "Quebec"
  },
  {
      "Name": "Shawinigan",
      "Province": "Quebec"
  },
  {
      "Name": "Sherbrooke",
      "Province": "Quebec"
  },
  {
      "Name": "Sorel-Tracy",
      "Province": "Quebec"
  },
  {
      "Name": "Stanstead",
      "Province": "Quebec"
  },
  {
      "Name": "Sutton",
      "Province": "Quebec"
  },
  {
      "Name": "Témiscaming",
      "Province": "Quebec"
  },
  {
      "Name": "Témiscouata-sur-le-Lac",
      "Province": "Quebec"
  },
  {
      "Name": "Terrebonne",
      "Province": "Quebec"
  },
  {
      "Name": "Thetford Mines",
      "Province": "Quebec"
  },
  {
      "Name": "Thurso",
      "Province": "Quebec"
  },
  {
      "Name": "Trois-Pistoles",
      "Province": "Quebec"
  },
  {
      "Name": "Trois-Rivières",
      "Province": "Quebec"
  },
  {
      "Name": "Valcourt",
      "Province": "Quebec"
  },
  {
      "Name": "Val-d'Or",
      "Province": "Quebec"
  },
  {
      "Name": "Varennes",
      "Province": "Quebec"
  },
  {
      "Name": "Vaudreuil-Dorion",
      "Province": "Quebec"
  },
  {
      "Name": "Victoriaville",
      "Province": "Quebec"
  },
  {
      "Name": "Ville-Marie",
      "Province": "Quebec"
  },
  {
      "Name": "Warwick",
      "Province": "Quebec"
  },
  {
      "Name": "Waterloo",
      "Province": "Quebec"
  },
  {
      "Name": "Waterville",
      "Province": "Quebec"
  },
  {
      "Name": "Westmount",
      "Province": "Quebec"
  },
  {
      "Name": "Windsor",
      "Province": "Quebec"
  },
  {
      "Name": "Estevan",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Flin Flon (part)",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Humboldt",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Lloydminster (part)",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Martensville",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Meadow Lake",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Melfort",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Melville",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Moose Jaw",
      "Province": "Saskatchewan"
  },
  {
      "Name": "North Battleford",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Prince Albert",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Regina",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Saskatoon",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Swift Current",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Warman",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Weyburn",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Yorkton",
      "Province": "Saskatchewan"
  },
  {
      "Name": "Whitehorse",
      "Province": "Yukon"
  }
]
export default cities;