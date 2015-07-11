MyApp.ApplicationAdapter = DS.FixtureAdapter.extend();

MyApp.Blueprint = DS.Model.extend({
    name: DS.attr("string"),
    group: DS.attr("string")
});

MyApp.Blueprint.FIXTURES = [
 {
     id: 1,
     name: "Stiletto",
     group: "Ship"
 },
 {
     id: 2,
     name: "Ares",
     group: "Ship"
 },
 {
     id: 3,
     name: "Covert Ops",
     group: "Equipment"
 }
];