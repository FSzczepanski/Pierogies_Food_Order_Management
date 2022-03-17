export enum PositionCategoryEnumTranslation {
  "Brak" = 0,
  "Promocja" = 1,
  "Przystawki" = 2,
  "Zupy" = 3,
  "Dania Główne" = 4,
  "Wegetariańskie" = 5,
  "Napoje" = 6,
  "Nasze wyroby" = 7,
  "Desery" = 8,
}

export enum FormTypeEnumTranslation {
  "Wydarzenie" = 0,
  "Na miejscu" = 1,
  "Dostawa" = 2,
}

export enum PaymentMethodEnumTranslation {
  "Na miejscu" = 0,
  "Przelewy24" = 1,
}

export function toArray(enumObject: any) {
  return Object.keys(enumObject)
    .filter((key) => typeof enumObject[key] === "number")
    .map((key) => ({ id: enumObject[key], name: key }));
}
