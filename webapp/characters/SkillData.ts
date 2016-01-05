export interface ISkillGrouping {
    section1: string[];
    section2: string[];
}

export var frigates: ISkillGrouping = {
    section1: ["Interceptors", "Covert Ops", "Assault Frigates", "Logistics Frigates"],
    section2: ["Amarr Frigate", "Caldari Frigate", "Gallente Frigate", "Minmatar Frigate"]
};

export var destroyers: ISkillGrouping = {
    section1: ["Command destroyers", "Interdictors"],
    section2: ["Amarr Destroyer", "Caldari Destroyer", "Gallente Destroyer", "Minmatar Destroyer"]
};

export var cruisers: ISkillGrouping = {
    section1: ["Heavy Assault Cruisers", "Recon Ships", "Heavy Interdiction Cruisers", "Logistics Cruisers"],
    section2: ["Amarr Cruiser", "Caldari Cruiser", "Gallente Cruiser", "Minmatar Cruiser"]
};

export var battlecruisers: ISkillGrouping = {
    section1: ["Command Ships"],
    section2: ["Amarr Battlecruiser", "Caldari Battlecruiser", "Gallente Battlecruiser", "Minmatar Battlecruiser"]
};

export var battleships: ISkillGrouping = {
    section1: ["Black Ops", "Marauders"],
    section2: ["Amarr Battleship", "Caldari Battleship", "Gallente Battleship", "Minmatar Battleship"]
};
