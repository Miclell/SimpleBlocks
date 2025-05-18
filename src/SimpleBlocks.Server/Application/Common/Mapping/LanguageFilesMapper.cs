using SimpleBlocks.Server.Domain.Entities;
using SimpleBlocks.Shared.Dto;

namespace SimpleBlocks.Server.Application.Common.Mapping;

public static class LanguageFilesMapper
{
    public static LanguageFilesDto ToDto(LanguageFileSet entity)
    {
        return new LanguageFilesDto
        {
            Blocks = entity.BlocksJson,
            Semantics = entity.SemanticsJson
        };
    }

    public static LanguageFileSet ToEntity(string languageKey, string blocksJson, string semanticsJson)
    {
        return new LanguageFileSet
        {
            Id = Guid.NewGuid(),
            LanguageKey = languageKey,
            BlocksJson = blocksJson,
            SemanticsJson = semanticsJson
        };
    }
} 