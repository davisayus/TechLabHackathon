using LT.TechLabHackathon.Domain.Entities;
using LT.TechLabHackathon.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.TechLabHackathon.Shared.Helpers
{
    public static class ExtensionMethods
    {
        public static UserDto MapToDto(this User user)
        {
            return new UserDto(
                user.UserId,
                user.Name,
                user.Email,
                user.AuthDoublefactor,
                user.Picture,
                user.PhoneNumber,
                user.StatusId);
        }
        public static User MapTo(this UserCreateDto userCreateDto)
        {
            return new User()
            {
                Name = userCreateDto.Name,
                Email = userCreateDto.Email,
                AuthDoublefactor = userCreateDto.AuthDoublefactor,
                Picture = userCreateDto.Picture,
                PhoneNumber = userCreateDto.PhoneNumber,
                StatusId = 5, // Status 5: New pending by password 
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Password = string.Empty
            };
        }
        public static UserChallengeDto MapToDto(this UserChallenge userChallenge)
        {
            return new UserChallengeDto
            {
                UserChallengeId = userChallenge.UserChallengeId,
                UserId = userChallenge.UserId,
                ChallengeId = userChallenge.ChallengeId,
                Attemps = userChallenge.Attemps,
                Penalized = userChallenge.Penalized,
                Unlocked = userChallenge.Unlocked,
                UnlockedDate = userChallenge.UnlockedDate,
                UnlokedTime = userChallenge.UnlokedTime,

                User = userChallenge.User?.MapToDto() ?? new (),
                Challenge = userChallenge.Challenge?.MapToDto() ?? new ()
            };
        }
        public static UserChallenge MapTo(this UserChallengeCreateDto userChallengeCreate)
        {
            return new UserChallenge
            {
                UserId = userChallengeCreate.UserId,
                ChallengeId = userChallengeCreate.ChallengeId,
                Attemps = userChallengeCreate.Attemps,
                Penalized = userChallengeCreate.Penalized,
                Unlocked = userChallengeCreate.Unlocked,
                UnlockedDate = userChallengeCreate.UnlockedDate,
                UnlokedTime = userChallengeCreate.UnlokedTime
            };
        }
        public static UserChallengeHistoryDto MapToDto(this UserChallengeHistory challengeHistory)
        {
            return new UserChallengeHistoryDto
            {
                UserChallengeHistoryId = challengeHistory.UserChallengeHistoryId,
                ChallengeId = challengeHistory.ChallengeId,
                UserId = challengeHistory.UserId,
                StartDate = challengeHistory.StartDate,
                EndDate = challengeHistory.EndDate,
                StartTime = challengeHistory.StartTime,
                EndTime = challengeHistory.EndTime,
                Unlocked = challengeHistory.Unlocked
            };
        }
        public static UserChallengeHistory MapTo(this UserChallengeHistoryCreateDto historyCreate)
        {
            return new UserChallengeHistory
            {
                ChallengeId = historyCreate.ChallengeId,
                UserId = historyCreate.UserId,
                StartDate = historyCreate.StartDate,
                EndDate = historyCreate.EndDate,
                StartTime = historyCreate.StartTime,
                EndTime = historyCreate.EndTime,
                Unlocked = historyCreate.Unlocked
            };
        }
        public static UserScoreDto MapToDto(this UserScore userScore)
        {
            return new UserScoreDto
            {
                UserScoreId = userScore.UserScoreId,
                UserId = userScore.UserId,
                Score = userScore.Score,
                UnlockedChallenges = userScore.UnlockedChallenges
            };
        }
        public static UserScore MapTo(this UserScoreCreateDto userScoreCreate) 
        {
            return new UserScore
            {
                UserId = userScoreCreate.UserId,
                Score = userScoreCreate.Score,
                UnlockedChallenges = userScoreCreate.UnlockedChallenges
            };
        }

        public static ProgrammingLanguageReservedWordDto MapToDto(this ProgrammingLanguageReservedWord programmingLanguageReservedWord)
        {
            return new ProgrammingLanguageReservedWordDto(
                programmingLanguageReservedWord.ProgrammingLanguageReservedWordId,
                programmingLanguageReservedWord.ReservedWord
                );
        }
        public static ProgrammingLanguageReservedWord MapTo(this ProgrammingLanguageReservedWordCreateDto programmingLanguageReservedWordCreate)
        {
            return new ProgrammingLanguageReservedWord
            {
                ReservedWord = programmingLanguageReservedWordCreate.ReservedWord
            };
        }
        public static ProgrammingLanguageDataTypeDto MapToDto(this ProgrammingLanguageDataType programmingLanguageDataType)
        {
            return new ProgrammingLanguageDataTypeDto
            {
                ProgrammingLanguageDataTypeId = programmingLanguageDataType.ProgrammingLanguageDataTypeId,
                ProgrammingLanguageId = programmingLanguageDataType.ProgrammingLanguageId,
                DataTypeId = programmingLanguageDataType.DataTypeId,
                DataType = programmingLanguageDataType.DataType is null ? new GeneralDataTypeDto() : programmingLanguageDataType.DataType.MapToDto()
            };
        }
        public static ProgrammingLanguageDataType MapTo(this ProgrammingLanguageDataTypeCreateDto programmingLanguageDataTypeCreate)
        {
            return new ProgrammingLanguageDataType
            {
                ProgrammingLanguageId = programmingLanguageDataTypeCreate.ProgrammingLanguageId,
                DataTypeId = programmingLanguageDataTypeCreate.DataTypeId
            };
        }
        public static ProgrammingLanguageDto MapToDto(this ProgrammingLanguage programmingLanguage)
        {
            return new ProgrammingLanguageDto
            {
                ProgrammingLanguageId = programmingLanguage.ProgrammingLanguageId,
                Description = programmingLanguage.Description,
                Title = programmingLanguage.Title,
                Version = programmingLanguage.Version,
                ReservedWords = programmingLanguage.ReservedWords is null ? [] : programmingLanguage.ReservedWords.Select(r => r.MapToDto()).ToList(),
                DataTypes = programmingLanguage.DataTypes is null ? [] : programmingLanguage.DataTypes.Select(dt => dt.MapToDto()).ToList()
            };
        }
        public static ProgrammingLanguage MapTo(this ProgrammingLanguageCreateDto programmingLanguageCreateDto)
        {
            List<ProgrammingLanguageReservedWord> reservedWords = [];
            foreach (var word in programmingLanguageCreateDto.ReservedWords)
                reservedWords.Add(new ProgrammingLanguageReservedWord() { ReservedWord = word });

            return new ProgrammingLanguage()
            {
                Description = programmingLanguageCreateDto.Description,
                Title = programmingLanguageCreateDto.Title,
                Version = programmingLanguageCreateDto.Version,
                ReservedWords = reservedWords
            };
        }

        public static ChallengeLevelDto MapToDto(this ChallengeLevel challengeLevel)
        {
            return new ChallengeLevelDto
            {
                LevelId = challengeLevel.LevelId,
                Description = challengeLevel.Description,
                Score = challengeLevel.Score
            };
        }
        public static ChallengeLevel MapTo(this ChallengeLevelCreateDto challengeLevelCreateDto)
        {
            return new ChallengeLevel
            {
                Description = challengeLevelCreateDto.Description,
                Score = challengeLevelCreateDto.Score
            };
        }
        public static ChallengeDto MapToDto(this Challenge challenge)
        {
            return new ChallengeDto
            {
                ChallengeId = challenge.ChallengeId,
                Description = challenge.Description,
                Title = challenge.Title,
                LevelId = challenge.LevelId,
                MethodName = challenge.MethodName,
                InputParameters = challenge.InputParameters,
                ResultDataTypeId = challenge.ResultDataTypeId,

                Constraints = challenge.Constraints?.Select(c => c.MapToDto()).ToList() ?? [],
                Examples = challenge.Examples?.Select(e => e.MapToDto()).ToList() ?? [],
                Validations = challenge.Validations?.Select(v => v.MapToDto()).ToList() ?? [],
                LanguageSignatures = challenge.LanguageSignatures?.Select(s => s.MapToDto()).ToList() ?? [],
                InputSetupParameters = challenge.InputSetupParameters?.Select(isp=>isp.MapToDto()).ToList() ?? [],
                DataType = challenge.DataType.MapToDto() ?? new GeneralDataTypeDto(),
            };
        }
        public static Challenge MapTo(this ChallengeCreateDto challengeCreate)
        {
            return new Challenge
            {
                Description = challengeCreate.Description,
                Title = challengeCreate.Title,
                LevelId = challengeCreate.LevelId,
                MethodName = challengeCreate.MethodName,
                InputParameters = challengeCreate.InputParameters,
                ResultDataTypeId = challengeCreate.ResultDataTypeId,

                
            };
        }
        public static ChallengeConstraintDto MapToDto(this ChallengeConstraint constraint)
        {
            return new ChallengeConstraintDto
                {
                    ConstraintId = constraint.ConstraintId,
                    ChallengeId = constraint.ChallengeId,
                    Description = constraint.Description
                };
        }
        public static ChallengeConstraint MapTo(this ChallengeConstraintCreateDto constraintCreate)
        {
            return new ChallengeConstraint
            {
                ChallengeId = constraintCreate.ChallengeId,
                Description = constraintCreate.Description
            };
        }
        public static ChallengeExampleDto MapToDto(this ChallengeExample example)
        {
            return new ChallengeExampleDto
            {
                ExampleId = example.ExampleId,
                ChallengeId = example.ChallengeId,
                ValidationId = example.ValidationId,
                Explanation = example.Explanation
            };
        }
        public static ChallengeExample MapTo(this ChallengeExampleCreateDto exampleCreate)
        {
            return new ChallengeExample
            {
                ChallengeId = exampleCreate.ChallengeId,
                ValidationId = exampleCreate.ValidationId,
                Explanation = exampleCreate.Explanation
            };
        }
        public static ChallengeValidationDto MapToDto(this ChallengeValidation validation)
        {
            return new ChallengeValidationDto
            {
                ValidationId = validation.ValidationId,
                ChallengeId = validation.ChallengeId,
                OutputValue = validation.OutputValue,
                InputParameters = validation.InputParameters?.Select(ip=>ip.MapToDto()).ToList() ?? []
            };
        }
        public static ChallengeValidation MapTo(this ChallengeValidationCreateDto validationCreate)
        {
            return new ChallengeValidation
            {
                ChallengeId = validationCreate.ChallengeId,
                OutputValue = validationCreate.OutputValue
            };
        }
        public static ChallengeLanguageSignatureDto MapToDto(this ChallengeLanguageSignature languageSignature)
        {
            return new ChallengeLanguageSignatureDto
            {
                LanguageSignatureId = languageSignature.LanguageSignatureId,
                ChallengeId = languageSignature.ChallengeId,
                ProgrammingLanguageId = languageSignature.ProgrammingLanguageId,
                Signature = languageSignature.Signature
            };
        }
        public static ChallengeLanguageSignature MapTo(this ChallengeLanguageSignatureCreateDto signatureCreate)
        {
            return new ChallengeLanguageSignature
            {
                ChallengeId = signatureCreate.ChallengeId,
                ProgrammingLanguageId = signatureCreate.ProgrammingLanguageId,
                Signature = signatureCreate.Signature
            };
        }
        public static ChallengeInputParameterDto MapToDto(this ChallengeInputParameter inputParameter)
        {
            return new ChallengeInputParameterDto
            {
                InputParameterId = inputParameter.InputParameterId,
                ValidationId = inputParameter.ValidationId,
                Sequence = inputParameter.Sequence,
                InputValue = inputParameter.InputValue
            };
        }
        public static ChallengeInputParameter MapTo(this ChallengeInputParameterCreateDto parameterCreate)
        {
            return new ChallengeInputParameter
            {
                ValidationId = parameterCreate.ValidationId,
                Sequence = parameterCreate.Sequence,
                InputValue = parameterCreate.InputValue
            };
        }
        public static ChallengeInputSetupParameterDto MapToDto(this ChallengeInputSetupParameter inputSetupParameter)
        {
            return new ChallengeInputSetupParameterDto
            {
                ChallengeInputSetupParameterId = inputSetupParameter.ChallengeInputSetupParameterId,
                ChallengeId = inputSetupParameter.ChallengeId,
                Sequence = inputSetupParameter.Sequence,
                DataTypeId = inputSetupParameter.DataTypeId,
                ParameterName = inputSetupParameter.ParameterName,
                DataType = inputSetupParameter.DataType.MapToDto() ?? new GeneralDataTypeDto()
            };
        }
        public static ChallengeInputSetupParameter MapTo(this ChallengeInputSetupParameterCreateDto setupParameterCreate)
        {
            return new ChallengeInputSetupParameter
            {
                ChallengeId = setupParameterCreate.ChallengeId,
                Sequence = setupParameterCreate.Sequence,
                DataTypeId = setupParameterCreate.DataTypeId,
                ParameterName = setupParameterCreate.ParameterName
            };
        }
        public static GeneralDataTypeDto MapToDto(this GeneralDataType dataType)
        {
            return new GeneralDataTypeDto
            {
                DataTypeId = dataType.DataTypeId,
                Name = dataType.Name,
                Description = dataType.Description
            };
        }
        public static GeneralDataType MapTo(this GeneralDataTypeCreateDto generalDataTypeCreate)
        {
            return new GeneralDataType
            {
                Name = generalDataTypeCreate.Name,
                Description = generalDataTypeCreate.Description,
            };
        }

        public static StatusDto MapToDto(this Status status)
        {
            return new StatusDto
            {
                StatusId = status.StatusId,
                Description = status.Description
            };
        }
        public static Status MapTo(this StatusCreateDto statusCreate)
        {
            return new Status
            {
                Description = statusCreate.Description
            };
        }

        public static Q MapperTo<T, Q>(T source) where T : class, new() where Q : class, new()
        {
            if (source is null) return new ();

            if (typeof(T) == typeof(ProgrammingLanguage))
            {
                var sourceDefined = source as ProgrammingLanguage;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ProgrammingLanguageCreateDto))
            {
                var sourceDefined = source as ProgrammingLanguageCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ProgrammingLanguageDataType))
            {
                var sourceDefined = source as ProgrammingLanguageDataType;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ProgrammingLanguageDataTypeCreateDto))
            {
                var sourceDefined = source as ProgrammingLanguageDataTypeCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ProgrammingLanguageReservedWord))
            {
                var sourceDefined = source as ProgrammingLanguageReservedWord;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ProgrammingLanguageReservedWordCreateDto))
            {
                var sourceDefined = source as ProgrammingLanguageReservedWordCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }

            else if (typeof(T) == typeof(User))
            {
                var sourceDefined = source as User;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(UserCreateDto))
            {
                var sourceDefined = source as UserCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(UserChallenge))
            {
                var sourceDefined = source as UserChallenge;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(UserChallengeCreateDto))
            {
                var sourceDefined = source as UserChallengeCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(UserChallengeHistory))
            {
                var sourceDefined = source as UserChallengeHistory;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(UserChallengeHistoryCreateDto))
            {
                var sourceDefined = source as UserChallengeHistoryCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(UserScore))
            {
                var sourceDefined = source as UserScore;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(UserScoreCreateDto))
            {
                var sourceDefined = source as UserScoreCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }

            else if (typeof(T) == typeof(ChallengeLevel))
            {
                var sourceDefined = source as ChallengeLevel;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeLevelCreateDto))
            {
                var sourceDefined = source as ChallengeLevelCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(Challenge))
            {
                var sourceDefined = source as Challenge;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeCreateDto))
            {
                var sourceDefined = source as ChallengeCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeConstraint))
            {
                var sourceDefined = source as ChallengeConstraint;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeConstraintCreateDto))
            {
                var sourceDefined = source as ChallengeConstraintCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeExample))
            {
                var sourceDefined = source as ChallengeExample;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeExampleCreateDto))
            {
                var sourceDefined = source as ChallengeExampleCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeInputParameter))
            {
                var sourceDefined = source as ChallengeInputParameter;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeInputParameterCreateDto))
            {
                var sourceDefined = source as ChallengeInputParameterCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeInputSetupParameter))
            {
                var sourceDefined = source as ChallengeInputSetupParameter;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeInputSetupParameterCreateDto))
            {
                var sourceDefined = source as ChallengeInputSetupParameterCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeLanguageSignature))
            {
                var sourceDefined = source as ChallengeLanguageSignature;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeLanguageSignatureCreateDto))
            {
                var sourceDefined = source as ChallengeLanguageSignatureCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeValidation))
            {
                var sourceDefined = source as ChallengeValidation;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(ChallengeValidationCreateDto))
            {
                var sourceDefined = source as ChallengeValidationCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }

            else if (typeof(T) == typeof(GeneralDataType))
            {
                var sourceDefined = source as GeneralDataType;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(GeneralDataTypeCreateDto))
            {
                var sourceDefined = source as GeneralDataTypeCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }

            else if (typeof(T) == typeof(Status))
            {
                var sourceDefined = source as Status;
                return sourceDefined?.MapToDto() as Q ?? new Q();
            }
            else if (typeof(T) == typeof(StatusCreateDto))
            {
                var sourceDefined = source as StatusCreateDto;
                return sourceDefined?.MapTo() as Q ?? new Q();
            }

            else
                throw new Exception($"Mapper not defined Source:{typeof(T).Name} to : {typeof(Q).Name}");
            
        }
    }
}
