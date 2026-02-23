const RESOURCE_NAMES = [
	"categories",
	"auth",
	"users",
	"series",
	"products",
	"carts",
	"orders",
	"campaigns",
	"addresses",
	"dashboard",
	"benefits",
	"members",
	"memberships",
	"params",
] as const

type TResourceUnion = (typeof RESOURCE_NAMES)[number]
type TResourceUppers = Uppercase<TResourceUnion>
type TResourceVariants = {
	BASE: string
	PUBLIC: string
	PRIVATE: string
	PUBLIC_ADMIN: string
	PRIVATE_ADMIN: string
}

export type TResources = Record<
	TResourceUppers,
	TResourceVariants
>

export const RESOURCES = RESOURCE_NAMES.reduce(
	(acc, key) => {
		const upper = key.toUpperCase() as TResourceUppers
		acc[upper] = {
			BASE: `/${key}`,
			PUBLIC: `/public/${key}`,
			PRIVATE: `/${key}`,
			PUBLIC_ADMIN: `/public/admin/${key}`,
			PRIVATE_ADMIN: `/admin/${key}`,
		}
		return acc
	},
	{} as TResources
)
