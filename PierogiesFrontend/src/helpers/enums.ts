export enum PositionCategoryEnumTranslation {
  "Brak" = 0,
  "Promocja" = 1,
  "Przystawka" = 2,
  "Zupa" = 3,
  "Danie Główne" = 4,
  "Wegetariańskie" = 5,
  "Napoje" = 6,
  "Nasze wyroby" = 7,
  "Desery" = 8,
}

export function toArray(enumObject: any) {
  return Object.keys(enumObject)
      .filter(key => typeof enumObject[key] === "number")
      .map(key => ({ id: enumObject[key], name: key }));
};