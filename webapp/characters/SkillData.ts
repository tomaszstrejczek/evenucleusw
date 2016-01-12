export interface ISkillGrouping {
    section1: string[];
    section2: Array<string[]>;
}

export var frigates: ISkillGrouping = {
    section1: ["Interceptors", "Covert Ops", "Assault Frigates", "Logistics Frigates"],
    section2: [["Amarr Frigate", "Caldari Frigate", "Gallente Frigate", "Minmatar Frigate"]]
};

export var destroyers: ISkillGrouping = {
    section1: ["Command destroyers", "Interdictors"],
    section2: [["Amarr Destroyer", "Caldari Destroyer", "Gallente Destroyer", "Minmatar Destroyer"]]
};

export var cruisers: ISkillGrouping = {
    section1: ["Heavy Assault Cruisers", "Recon Ships", "Heavy Interdiction Cruisers", "Logistics Cruisers"],
    section2: [["Amarr Cruiser", "Caldari Cruiser", "Gallente Cruiser", "Minmatar Cruiser"]]
};

export var battlecruisers: ISkillGrouping = {
    section1: ["Command Ships"],
    section2: [["Amarr Battlecruiser", "Caldari Battlecruiser", "Gallente Battlecruiser", "Minmatar Battlecruiser"]]
};

export var battleships: ISkillGrouping = {
    section1: ["Black Ops", "Marauders"],
    section2: [["Amarr Battleship", "Caldari Battleship", "Gallente Battleship", "Minmatar Battleship"]]
};

export var transport: ISkillGrouping = {
    section1: ["Industry", "Transport Ships"],
    section2: [["Amarr Industrial", "Caldari Industrial", "Gallente Industrial", "Minmatar Industrial"]]
};

export var carriers: ISkillGrouping = {
    section1: ["Capital Ships", "Advanced Spaceship Command"],
    section2: [["Amarr Carrier", "Caldari Carrier", "Gallente Carrier", "Minmatar Carrier"]]
};

export var dreads: ISkillGrouping = {
    section1: ["Capital Ships", "Jump Drive Operation", "Advanced Weapon Upgrades"],
    section2: [["Amarr Dreadnought", "Caldari Dreadnought", "Gallente Dreadnought", "Minmatar Dreadnought"]]
};

export var freighters: ISkillGrouping = {
    section1: ["Advanced Spaceship Command"],
    section2: [["Amarr Freighter", "Caldari Freighter", "Gallente Freighter", "Minmatar Freighter"]]
};

export var jfreighters: ISkillGrouping = {
    section1: ["Jump Drive Operation", "Jump Drive Calibration", "Jump Freighters"],
    section2: [["Amarr Freighter", "Caldari Freighter", "Gallente Freighter", "Minmatar Freighter"]]
};

export var titans: ISkillGrouping = {
    section1: ["Capital Ships", "Jump Drive Operation", "Jump Portal Generation", "Astrometrics"],
    section2: [["Amarr Titan", "Caldari Titan", "Gallente Titan", "Minmatar Titan"]]
};

export var industryCapitals: ISkillGrouping = {
    section1: ["Capital Ships", "Jump Drive Operation", "Advanced Mass Production", "Capital Industrial Ships"],
    section2: [["Mining Foreman", "Mining Director", "ORE Industrial", "Industrial Reconfiguration"]]
};

export var barges: ISkillGrouping = {
    section1: ["Astrogeology", "Mining Barge", "Mining Frigate"],
    section2: [["Exhumers"]]
};

export var armor: ISkillGrouping = {
    section1: ["Mechanics", "Armor Layering", "Hull Upgrades"],
    section2: [["EM Armor Compensation", "Explosive Armor Compensation", "Kinetic Armor Compensation", "Thermal Armor Compensation"]]
};

export var shield: ISkillGrouping = {
    section1: ["Shield Management", "Shield Operation", "Shield Upgrades"],
    section2: [["EM Shield Compensation", "Explosive Shield Compensation", "Kinetic Shield Compensation", "Thermal Shield Compensation"]]
};

export var engineering: ISkillGrouping = {
    section1: ["CPU Management", "Power Grid Management", "Capacitor Management", "Weapon Upgrades"],
    section2: [["Advanced Weapon Upgrades", "Capacitor Systems Operation", "Electronics Upgrades", "Energy Grid Upgrades", "Thermodynamics"]]
};

export var rigging: ISkillGrouping = {
    section1: ["Jury Rigging", "Astronautics Rigging", "Armor Rigging", "Shield Rigging", "Drones Rigging"],
    section2: [["Launcher Rigging", "Projectile Weapon Rigging", "Hybrid Weapon Rigging", "Energy Weapon Rigging"]]
};

export var drones: ISkillGrouping = {
    section1: ["Drones", "Drone Navigation", "Drone Avionics", "Drone Sharpshooting", "Drone Durability", "Drone Interfacing"],
    section2: [["Light Drone Operation", "Medium Drone Operation", "Heavy Drone Operation", "Sentry Drone Interfacing", "Fighters", "Fighter Bombers"]]
};

export var projectiles: ISkillGrouping = {
    section1: ["Gunnery", "Motion Prediction", "Rapid Firing", "Sharpshooter", "Surgical Strike", "Trajectory Analysis", "Controlled Bursts"],
    section2: [["Small Projectile Turret", "Medium Projectile Turret", "Large Projectile Turret", "Capital Projectile Turret"],
        ["Small Hybrid Turret", "Medium Hybrid Turret", "Large Hybrid Turret", "Capital Hybrid Turret"],
        ["Small Energy Turret", "Medium Energy Turret", "Large Energy Turret", "Capital Energy Turret"],
    ]
};

export var missiles: ISkillGrouping = {
    section1: ["Missile Launcher Operation", "Missile Bombardment", "Missile Projection", "Rapid Launch", "Target Navigation Prediction", "Warhead Upgrades"],
    section2: [["Rockets", "Light Missiles", "Heavy Assault Missiles", "Heavy Missiles", "Cruise Missiles", "Citadel Cruise Missiles"],
        ["Bomb Deployment", "Torpedoes"]
    ]
};

export var amarrTech3: ISkillGrouping = {
    section1: ["Amarr Strategic Cruiser", "Amarr Cruiser"],
    section2: [["Amarr Defensive Systems", "Amarr Electronic Systems", "Amarr Engineering Systems", "Amarr Offensive Systems", "Amarr Propulsion Systems"]]
};

export var caldariTech3: ISkillGrouping = {
    section1: ["Caldari Strategic Cruiser", "Caldari Cruiser"],
    section2: [["Caldari Defensive Systems", "Caldari Electronic Systems", "Caldari Engineering Systems", "Caldari Offensive Systems", "Caldari Propulsion Systems"]]
};

export var gallenteTech3: ISkillGrouping = {
    section1: ["Gallente Strategic Cruiser", "Gallente Cruiser"],
    section2: [["Gallente Defensive Systems", "Gallente Electronic Systems", "Gallente Engineering Systems", "Gallente Offensive Systems", "Gallente Propulsion Systems"]]
};

export var minmatarTech3: ISkillGrouping = {
    section1: ["Minmatar Strategic Cruiser", "Minmatar Cruiser"],
    section2: [["Minmatar Defensive Systems", "Minmatar Electronic Systems", "Minmatar Engineering Systems", "Minmatar Offensive Systems", "Minmatar Propulsion Systems"]]
};
