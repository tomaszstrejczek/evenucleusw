
 
 

 

/// <reference path="Enums.ts" />

declare module ts.dto {
	interface KeyInfoDto {
		keyInfoId: number;
		keyId: number;
		vCode: string;
		userId: number;
		pilots: ts.dto.PilotDto[];
		corporations: ts.dto.CorporationDto[];
	}
	interface PilotDto {
		pilotId: number;
		eveId: number;
		name: string;
		url: string;
		currentTrainingNameAndLevel: string;
		currentTrainingEnd: Date;
		trainingQueueEnd: Date;
		trainingActive: boolean;
		maxManufacturingJobs: number;
		maxResearchJobs: number;
		freeManufacturingJobsNofificationCount: number;
		freeResearchJobsNofificationCount: number;
		keyInfoId: number;
		userId: number;
		trainingWarning: boolean;
		trainingNotActive: boolean;
		skills: ts.dto.SkillDto[];
		skillsInQueue: ts.dto.SkillInQueueDto[];
	}
	interface SkillDto {
		skillId: number;
		pilotId: number;
		skillName: string;
		level: number;
	}
	interface SkillInQueueDto {
		skillInQueueId: number;
		pilotId: number;
		skillName: string;
		level: number;
		length: string;
	}
	interface CorporationDto {
		corporationId: number;
		eveId: number;
		name: string;
		keyInfoId: number;
		userId: number;
		url: string;
	}
	interface Error {
		errorMessage: string;
		fullException: string;
		errors: string[];
	}
	interface SingleStringDto {
		value: string;
	}
	interface SingleLongDto {
		value: number;
	}
}


