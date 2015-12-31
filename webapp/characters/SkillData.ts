export interface ISkillGrouping {
    section1: string[];
    section2: string[];
}

export var frigates: ISkillGrouping = {
    section1: ["Interceptors", "Covert Ops", "Assault Frigates", "Logistics Frigates"],
    section2: ["Amarr Frigates", "Caldari Frigates", "Gallente Frigates", "Minmatar Frigates"]
};

export var destroyers: ISkillGrouping = {
    section1: ["Command destroyers", "Interdictors"],
    section2: ["Amarr Destroyers", "Caldari Destroyers", "Gallente Destroyers", "Minmatar Destroyers"]
};

