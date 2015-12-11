
 
 

 

/// <reference path="Enums.ts" />

declare module ts.dto {
	interface KeyInfoDto {
		KeyInfoId: number;
		KeyId: number;
		VCode: string;
		UserId: number;
		Pilots: ts.dto.PilotDto[];
		Corporations: ts.dto.CorporationDto[];
	}
	interface PilotDto {
		PilotId: number;
		EveId: number;
		Name: string;
		Url: string;
		CurrentTrainingNameAndLevel: string;
		CurrentTrainingEnd: Date;
		TrainingQueueEnd: Date;
		TrainingActive: boolean;
		MaxManufacturingJobs: number;
		MaxResearchJobs: number;
		FreeManufacturingJobsNofificationCount: number;
		FreeResearchJobsNofificationCount: number;
		KeyInfoId: number;
		UserId: number;
		TrainingWarning: boolean;
		TrainingNotActive: boolean;
	}
	interface CorporationDto {
		CorporationId: number;
		EveId: number;
		Name: string;
		KeyInfoId: number;
		UserId: number;
		Url: string;
	}
}


