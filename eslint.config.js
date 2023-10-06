import { FlatCompat } from "@eslint/eslintrc";
import eslintJs from "@eslint/js";
import prettierConfiguration from "eslint-config-prettier";
import importPlugin from "eslint-plugin-import";
import { ruleSeverity, ruleCompliance } from "./linter-configuration.js";

const eslintJsConfiguration = eslintJs.configs.recommended;
const compatibilityManager = new FlatCompat();
const airbnbConfiguration = compatibilityManager.extends("airbnb-base");
const rootConfiguration = [
	eslintJsConfiguration,
	...airbnbConfiguration,
	prettierConfiguration,
	{
		plugins: {
			importPlugin
		},
		rules: {
			"import/extensions": [ruleSeverity.error, ruleCompliance.always],
			"import/no-extraneous-dependencies": ruleSeverity.disabled,
			"import/exports-last": ruleSeverity.error
		}
	}
];

export default rootConfiguration;
